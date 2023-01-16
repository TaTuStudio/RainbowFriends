using UnityEngine;

public class UI_Input_Controller : MonoBehaviour
{
    public static UI_Input_Controller instance;

    public VariableJoystick moveJoyStick;

    public UI_Rotate_Camera_View uI_Rotate_Camera;

    private void Awake()
    {
        instance = this;
    }

    public void _HideButton()
    {
        if((!ReferenceEquals(PlayerManager.instance.spawnedPlayer,null)))
        {
            PlayerManager.instance.spawnedPlayer._SetHide();
        }
    }
}
