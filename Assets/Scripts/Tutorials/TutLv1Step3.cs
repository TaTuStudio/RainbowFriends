using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutLv1Step3 : MonoBehaviour
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
        if (stepDone == false && GameController.instance.gameplaySetupDone && PlayerManager.instance.spawnedPlayer != null && PlayerManager.instance.spawnedPlayer.isHiding)
        {
            stepDone = true;

            tutController._NextStep();
        }
    }
}
