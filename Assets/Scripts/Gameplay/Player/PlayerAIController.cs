using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Pathfinding;

public class PlayerAIController : MonoBehaviour
{
    public AIPath aIPath;

    float speed = 4f;

    public bool isHiding = false;

    public bool catched = false;

    public bool isDead = false;

    public bool setDefault = false;

    public Transform OnHandItemContainer_Right;

    public List<Transform> rightHandCollectedList = new List<Transform>();

    public SoundEffectSO deadSfx;
    
    public delegate void AIDiedAction();
    public static event AIDiedAction OnAIDied;
    
    private void OnEnable()
    {
        setDefault = true;
    }

    private void Awake()
    {
        aIPath = GetComponent<AIPath>();
    }

    private void Update()
    {
        _Default();

        _SetAnimMove();
    }

    public void _Default()
    {
        if (setDefault == false) return;

        setDefault = false;

        isHiding = false;
        catched = false;
        isDead = false;

        _CleanItems();

        _SetHoldItemAnim(false);

        _SetDeadAnim(isDead);

        _SetHideAnim(isHiding);

        aIPath.isStopped = false;
    }

    void _CleanItems()
    {
        foreach (Transform t in CollectionMarshal.AsSpan(rightHandCollectedList))
        {
            if (t == null) continue;

            ReuseGO reuseGO = t.GetComponent<ReuseGO>();

            if (reuseGO != null)
            {
                UnusedManager.instance._AddToUnusedGO(reuseGO);
            }
        }

        rightHandCollectedList.Clear();
    }

    public void _AddRightHandColectItem(Transform item)
    {
        rightHandCollectedList.Add(item);

        _SetHoldItemAnim(true);
    }

    public void _RemoveRightHandColectItem(Transform item)
    {
        rightHandCollectedList.Remove(item);

        if (rightHandCollectedList.Count <= 0)
        {
            _SetHoldItemAnim(false);
        }
    }

    public void _SetHide()
    {
        if (catched || isDead) return;

        if (isHiding == false)
        {
            isHiding = true;

            _SetHideAnim(isHiding);
        }
        else
        {
            isHiding = false;

            _SetHideAnim(isHiding);
        }
    }

    public void _SetCatched(bool active)
    {
        if (active)
        {
            catched = true;

            aIPath.isStopped = true;

            //aIPath._SetMoveToPosition(transform.position);
        }
        else
        {
            catched = false;

            aIPath.isStopped = false;
        }
    }

    public void _SetHit()
    {
        isDead = true;
        
        aIPath._SetMoveToPosition(transform.position);

        _SetDeadAnim(true);
        
        deadSfx.Play(gameObject);
        
        OnAIDied?.Invoke();
    }

    #region Animations

    public Animator playerAnimator;

    void _SetAnimMove()
    {
        if (GameController.instance.isPlaying == false || catched || isDead) return;

        float curSpeed = speed;

        if (isHiding)
        {
            curSpeed = speed / 2f;
        }

        aIPath.maxSpeed = curSpeed;

        // Calculate the velocity relative to this transform's orientation
        Vector3 relVelocity = transform.InverseTransformDirection(aIPath.velocity);
        relVelocity.y = 0;

        // Speed relative to the character size
        playerAnimator.SetFloat("NormalizedSpeed", relVelocity.magnitude / playerAnimator.transform.lossyScale.x);
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
}
