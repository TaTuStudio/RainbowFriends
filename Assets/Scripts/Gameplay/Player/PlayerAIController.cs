using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIController : MonoBehaviour
{
    public bool isHiding = false;

    public bool catched = false;

    public bool isDead = false;

    public bool setDefault = false;

    public Transform OnHandItemContainer_Right;

    public List<Transform> rightHandCollectedList = new List<Transform>();

    private void Update()
    {
        _Default();

        _UpdateMovementAnim();
    }

    public void _Default()
    {
        if (setDefault == false) return;

        setDefault = false;

        isHiding = false;
        catched = false;
        isDead = false;

        rightHandCollectedList.Clear();

        _SetHoldItemAnim(false);

        _SetDeadAnim(false);
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

    public void _SetCatched()
    {
        catched = true;
    }

    public void _SetHit()
    {
        isDead = true;

        _SetDeadAnim(true);
    }

    #region Animations

    public Animator playerAnimator;

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

    #endregion
}
