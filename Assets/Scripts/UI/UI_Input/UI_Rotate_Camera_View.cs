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

    public int fingerId = -1;

    float modRotSpeedX = 0.1f;
    float modRotSpeedY = 0.1f;

    private void OnEnable()
    {
        _ResetDelta();
    }

    private void Update()
    {
        _Touch();
    }

    void _Touch()
    {
        if (isRotate == false) return;

        for(int i=0; i<Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        if (fingerId == -1)
                        {
                            fingerId = touch.fingerId;
                        }
                    }
                    break;

                case TouchPhase.Moved:
                    {

                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    {
                        if (touch.fingerId == fingerId)
                        {
                            _ResetDelta();
                        }
                    }
                    break;
            }

            if (fingerId != -1 && touch.fingerId == fingerId)
            {
                deltaX = touch.deltaPosition.x * modRotSpeedX;
                deltaY = touch.deltaPosition.y * modRotSpeedY;

                valueText.text = "Current Value: " + new Vector2(deltaX, deltaY);
            }
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
        _ResetDelta();
    }

    void _OnMouseDown()
    {
        isRotate = true;

        fingerId = -1;
    }

    void _ResetDelta()
    {
        deltaX = 0f;
        deltaY = 0f;

        isRotate = false;

        fingerId = -1;
    }
}
