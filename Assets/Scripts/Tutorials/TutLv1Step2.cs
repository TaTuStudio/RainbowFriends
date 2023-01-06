using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutLv1Step2 : MonoBehaviour
{
    public TutController tutController;

    public bool active = false;

    public bool stepDone = false;

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            active = false;

            stepDone = false;
        }

        _CheckMove();
    }

    private void OnEnable()
    {
        active = true;
    }

    void _CheckMove()
    {
        if (stepDone == false && GameController.instance.gameplaySetupDone && PlayerManager.instance.spawnedPlayer != null)
        {
            if (UI_Input_Controller.instance.uI_Rotate_Camera.deltaX > 0f || UI_Input_Controller.instance.uI_Rotate_Camera.deltaX > 0f)
            {
                stepDone = true;

                tutController._NextStep();
            }
        }
    }
}
