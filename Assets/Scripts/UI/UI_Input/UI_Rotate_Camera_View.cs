using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Rotate_Camera_View : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isRotate = false;
    public float deltaX;
    public float deltaY;

    public Text valueText;

    public bool getTouch = false;
    public int fingerId = -1;

    float modRotSpeedX = 0.1f;
    float modRotSpeedY = 0.1f;

    private void OnEnable()
    {
        _ResetDelta();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _Touch();
    }

    void _Touch()
    {
        if(Input.touchCount > 0 && isRotate)
        {
            Touch touch = Input.GetTouch(0);

            if (getTouch == true)
            {
                getTouch = false;

                fingerId = touch.fingerId;
            }

            //if (touch.phase == TouchPhase.Moved)
            //{

            //}

            if(touch.phase == TouchPhase.Ended)
            {
                _ResetDelta();
            }

            deltaX = touch.deltaPosition.x * modRotSpeedX;
            deltaY = touch.deltaPosition.y * modRotSpeedY;

            valueText.text = "Current Value: " + new Vector2(deltaX, deltaY);
        }
        else
        {
            _ResetDelta();
        }
    }

    public void _ChangeXValue(string val)
    {
        float.TryParse(val, out modRotSpeedX);
    }

    public void _ChangeYValue(string val)
    {
        float.TryParse(val, out modRotSpeedY);
    }

    public void OnUpdateSelected(BaseEventData data)
    {

    }
    public void OnPointerDown(PointerEventData data)
    {
        _OnMouseDown();
    }
    public void OnPointerUp(PointerEventData data)
    {

    }

    void _OnMouseDown()
    {
        isRotate = true;

        getTouch = true;

        fingerId = -1;
    }

    void _ResetDelta()
    {
        deltaX = 0f;
        deltaY = 0f;

        isRotate = false;
        getTouch = false;

        fingerId = -1;
    }
}
