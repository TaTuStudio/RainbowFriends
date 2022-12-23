using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyCharacterMovement;
using Cinemachine;
using Lofelt.NiceVibrations;

public class PlayerController : MonoBehaviour
{
    #region EDITOR EXPOSED FIELDS

    [Space(15f)]
    [Tooltip("The character's maximum speed.")]
    public static float maxSpeed = 4f;

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
        if (isDead) return;

        float yEuler = transform.localEulerAngles.y + UI_Input_Controller.instance.uI_Rotate_Camera.deltaX;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, yEuler, transform.localEulerAngles.z);

        fpsCamRotVertical -= UI_Input_Controller.instance.uI_Rotate_Camera.deltaY;
        fpsCamRotVertical = Mathf.Clamp(fpsCamRotVertical, -60f, 60f);
        fpsVirtualCam.transform.localEulerAngles = new Vector3(fpsCamRotVertical, 0f, 0f);
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

        Vector3 desiredVelocity = Vector3.zero;

        float curSpeed = maxSpeed;

        if (isHiding)
        {
            curSpeed = maxSpeed / 2f;
        }

        if (catched == false && isDead == false)
        {
            desiredVelocity = movementDirection * curSpeed;
        }

        // Update character�s velocity based on its grounding status

        if (characterMovement.isGrounded)
            GroundedMovement(desiredVelocity);
        else
            NotGroundedMovement(desiredVelocity);

        characterMovement.Move();

        //Step sound FX

        float vertical = Mathf.Abs(UI_Input_Controller.instance.moveJoyStick.Vertical);
        float horizontal = Mathf.Abs(UI_Input_Controller.instance.moveJoyStick.Horizontal);

        if (vertical > horizontal)
        {
            playerSFX._SetSpeedNormalize(vertical);
        }
        else
        {
            playerSFX._SetSpeedNormalize(horizontal);
        }
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

        setDefault = true;
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
    public CinemachineVirtualCamera tpsVirtualCam;
    public float fpsCamRotVertical = 0f;

    public bool isHiding = false;

    public bool catched = false;

    public bool isDead = false;

    public bool setDefault = false;

    public Transform OnHandItemContainer_Right;

    public List<Transform> rightHandCollectedList = new List<Transform>();

    public SoundEffectSO hitSfx;

    public PlayerSFX playerSFX;

    private void Start()
    {
        CameraManager.instance._RegisterVirtualCamera(fpsVirtualCam);
        CameraManager.instance._RegisterVirtualCamera(tpsVirtualCam);
    }

    private void Update()
    {
        _Default();

        _UpdateMovementAnim();

        _UnHideDelay();
    }

    public void _Default()
    {
        if (setDefault == false) return;

        setDefault = false;

        isHiding = false;
        catched = false;
        isDead = false;

        isHiding = false;

        CameraManager.instance._GameplaySwitchCam(fpsVirtualCam);

        _CleanItems();

        _SetHoldItemAnim(false);

        _SetDeadAnim(isDead);

        _SetHideAnim(isHiding);

        _HideMesh(false);

        GameplayUI.instance._ActiveFlashLight(true);
    }

    void _CleanItems()
    {
        foreach(Transform t in rightHandCollectedList)
        {
            if (t == null) continue;

            ReuseGO reuseGO = t.GetComponent<ReuseGO>();

            if(reuseGO != null)
            {
                UnusedManager.instance._AddToUnusedGO(reuseGO);
            }
        }

        rightHandCollectedList.Clear();
    }

    public void _AddRightHandColectItem(Transform item)
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
        
        rightHandCollectedList.Add(item);

        _SetHoldItemAnim(true);
    }

    public void _RemoveRightHandColectItem(Transform item)
    {
        rightHandCollectedList.Remove(item);

        if(rightHandCollectedList.Count <= 0)
        {
            _SetHoldItemAnim(false);
        }
    }

    public void _SetHide()
    {
        if (catched || isDead) return;

        if(isHiding == false)
        {
            isHiding = true;

            CameraManager.instance._GameplaySwitchCam(tpsVirtualCam);

            _SetHideAnim(isHiding);

            GameplayUI.instance._ActiveFlashLight(false);
        }
        else
        {
            isHiding = false;

            unHideDelayTime = 1f;

            _SetHideAnim(isHiding);

            GameplayUI.instance._ActiveFlashLight(true);
        }
    }

    float unHideDelayTime = 0f;
    public void _UnHideDelay()
    {
        if (unHideDelayTime > 0f)
        {
            unHideDelayTime -= Time.deltaTime;

            if(unHideDelayTime <= 0f)
            {
                fpsCamRotVertical = 0f;

                CameraManager.instance._GameplaySwitchCam(fpsVirtualCam);
            }
        }
    }

    public void _SetCatched(bool active)
    {
        if (active)
        {
            catched = true;

            _HideMesh(true);
        }
        else
        {
            catched = false;

            _HideMesh(false);

            CameraManager.instance._GameplaySwitchCam(fpsVirtualCam);
        }

    }

    public void _SetHit()
    {
        isDead = true;

        fpsCamRotVertical = 0f;

        CameraManager.instance._GameplaySwitchCam(fpsVirtualCam);

        _SetDeadAnim(isDead);

        hitSfx.Play(gameObject);
    }

    #region Animations

    public Animator playerAnimator;
    public Vector3 amatureScale = Vector3.zero;

    void _HideMesh(bool active)
    {
        if (active)
        {
            playerAnimator.transform.localScale = Vector3.zero;
        }
        else
        {
            playerAnimator.transform.localScale = amatureScale;
        }
    }

    void _UpdateMovementAnim()
    {
        float trueSpeed = 0f;

        if (catched == false && isDead == false)
        {
            float vertical = Mathf.Abs(UI_Input_Controller.instance.moveJoyStick.Vertical);
            float horizontal = Mathf.Abs(UI_Input_Controller.instance.moveJoyStick.Horizontal);

            if (vertical > horizontal)
            {
                trueSpeed = vertical;
            }
            else
            {
                trueSpeed = horizontal;
            }
        }

        playerAnimator.SetFloat("NormalizedSpeed", trueSpeed);
    }

    void _SetDeadAnim(bool active)
    {
        playerAnimator.SetBool("Dead", active);
    }

    void _SetHoldItemAnim(bool active)
    {
        playerAnimator.SetBool("Hold Item", active);
    }

    void _SetHideAnim(bool active)
    {
        playerAnimator.SetBool("Hiding", active);
    }

    #endregion

    #endregion
}
