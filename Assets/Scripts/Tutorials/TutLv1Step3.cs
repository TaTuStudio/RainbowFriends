using UnityEngine;

public class TutLv1Step3 : MonoBehaviour
{
    public TutController tutController;

    public bool active;

    public bool stepDone;

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
        if (stepDone == false && GameController.instance.gameplaySetupDone && (!ReferenceEquals(PlayerManager.instance.spawnedPlayer,null)) && PlayerManager.instance.spawnedPlayer.isHiding)
        {
            stepDone = true;

            tutController._NextStep();
        }
    }
}
