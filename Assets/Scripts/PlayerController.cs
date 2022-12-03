using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyCharacterMovement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    #region EDITOR EXPOSED FIELDS

    [Space(15f)]
    [Tooltip("The character's maximum speed.")]
    public static float maxSpeed = 5f;

    [Tooltip("Max Acceleration (rate of change of velocity).")]
    public float maxAcceleration = 20.0f;

    [Tooltip("Setting that affects movement control. Higher values allow faster changes in direction.")]
    public float groundFriction = 8.0f;

    [Tooltip("Friction to apply when falling.")]
    public float airFriction = 0.1f;

    [Range(0.0f, 1.0f)]
    [Tooltip("When falling, amount of horizontal movement control available to the character.\n" +
             "0 = no control, 1 = full control at max acceleration.")]
    public float airControl = 0.3f;

    [Tooltip("The character's gravity.")]
    public Vector3 gravity = Vector3.down * 9.81f;

    [Space(15f)]
    [Tooltip("Character's height when standing.")]
    public float standingHeight = 2.0f;

    #endregion

    #region FIELDS

    private Coroutine _lateFixedUpdateCoroutine;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Cached CharacterMovement component.
    /// </summary>

    public CharacterMovement characterMovement { get; private set; }

    /// <summary>
    /// Desired movement direction vector in world-space.
    /// </summary>

    public Vector3 movementDirection { get; set; }

    /// <summary>
    /// Jump input.
    /// </summary>

    #endregion

    #region EVENT HANDLERS

    /// <summary>
    /// Collided event handler.
    /// </summary>

    private void OnCollided(ref CollisionResult inHit)
    {
        //Debug.Log($"{name} collided with: {inHit.collider.name}");
    }

    /// <summary>
    /// FoundGround event handler.
    /// </summary>

    private void OnFoundGround(ref FindGroundResult foundGround)
    {
        //Debug.Log("Found ground...");

        // Determine if the character has landed

        if (!characterMovement.wasOnGround && foundGround.isWalkableGround)
        {
            //Debug.Log("Landed!");
        }
    }

    #endregion

    #region METHODS

    /// <summary>
    /// Update the character's rotation.
    /// </summary>

    private void UpdateRotation()
    {
        //Other solution
        float yEuler = transform.localEulerAngles.y + UI_Input_Controller.instance.uI_Rotate_Camera.deltaX;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yEuler, transform.localEulerAngles.z);

        camRotX -= UI_Input_Controller.instance.uI_Rotate_Camera.deltaY;
        camRotX = Mathf.Clamp(camRotX, -90f, 90f);

        fpsVirtualCam.transform.localEulerAngles = new Vector3(camRotX, 0f, 0f);
    }

    /// <summary>
    /// Move the character when on walkable ground.
    /// </summary>

    private void GroundedMovement(Vector3 desiredVelocity)
    {
        characterMovement.velocity = Vector3.Lerp(characterMovement.velocity, desiredVelocity,
            1f - Mathf.Exp(-groundFriction * Time.deltaTime));
    }

    /// <summary>
    /// Move the character when falling or on not-walkable ground.
    /// </summary>

    private void NotGroundedMovement(Vector3 desiredVelocity)
    {
        // Current character's velocity

        Vector3 velocity = characterMovement.velocity;

        // If moving into non-walkable ground, limit its contribution.
        // Allow movement parallel, but not into it because that may push us up.

        if (characterMovement.isOnGround && Vector3.Dot(desiredVelocity, characterMovement.groundNormal) < 0.0f)
        {
            Vector3 groundNormal = characterMovement.groundNormal;
            Vector3 groundNormal2D = groundNormal.onlyXZ().normalized;

            desiredVelocity = desiredVelocity.projectedOnPlane(groundNormal2D);
        }

        // If moving...

        if (desiredVelocity != Vector3.zero)
        {
            // Accelerate horizontal velocity towards desired velocity

            Vector3 horizontalVelocity = Vector3.MoveTowards(velocity.onlyXZ(), desiredVelocity,
                maxAcceleration * airControl * Time.deltaTime);

            // Update velocity preserving gravity effects (vertical velocity)

            velocity = horizontalVelocity + velocity.onlyY();
        }

        // Apply gravity

        velocity += gravity * Time.deltaTime;

        // Apply Air friction (Drag)

        velocity -= velocity * airFriction * Time.deltaTime;

        // Update character's velocity

        characterMovement.velocity = velocity;
    }

    /// <summary>
    /// Perform character movement.
    /// </summary>

    private void Move()
    {
        movementDirection = transform.forward * UI_Input_Controller.instance.moveJoyStick.Vertical + transform.right * UI_Input_Controller.instance.moveJoyStick.Horizontal;

        Vector3 desiredVelocity = movementDirection * maxSpeed;

        // Update character�s velocity based on its grounding status

        if (characterMovement.isGrounded)
            GroundedMovement(desiredVelocity);
        else
            NotGroundedMovement(desiredVelocity);

        characterMovement.Move();
    }

    /// <summary>
    /// Post-Physics update, used to sync our character with physics.
    /// </summary>

    private void OnLateFixedUpdate()
    {
        UpdateRotation();
        Move();
    }

    #endregion

    #region MONOBEHAVIOR

    private void Awake()
    {
        // Cache CharacterMovement component

        characterMovement = GetComponent<CharacterMovement>();

        // Enable default physic interactions

        characterMovement.enablePhysicsInteraction = true;
    }

    private void OnEnable()
    {
        // Start LateFixedUpdate coroutine

        if (_lateFixedUpdateCoroutine != null)
            StopCoroutine(_lateFixedUpdateCoroutine);

        _lateFixedUpdateCoroutine = StartCoroutine(LateFixedUpdate());

        // Subscribe to CharacterMovement events

        characterMovement.FoundGround += OnFoundGround;
        characterMovement.Collided += OnCollided;
    }

    private void OnDisable()
    {
        // Ends LateFixedUpdate coroutine

        if (_lateFixedUpdateCoroutine != null)
            StopCoroutine(_lateFixedUpdateCoroutine);

        // Un-Subscribe from CharacterMovement events

        characterMovement.FoundGround -= OnFoundGround;
        characterMovement.Collided -= OnCollided;
    }

    private IEnumerator LateFixedUpdate()
    {
        WaitForFixedUpdate waitTime = new WaitForFixedUpdate();

        while (true)
        {
            yield return waitTime;

            OnLateFixedUpdate();
        }

        yield break;
    }

    #endregion

    #region Custom

    [Header("Custom settings")]

    public CinemachineVirtualCamera fpsVirtualCam;
    public float camRotX = 0f;

    #endregion
}
