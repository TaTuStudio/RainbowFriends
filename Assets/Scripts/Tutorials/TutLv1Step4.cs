using UnityEngine;

public class TutLv1Step4 : MonoBehaviour
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
        if (stepDone || !GameController.instance.gameplaySetupDone ||
            (ReferenceEquals(PlayerManager.instance.spawnedPlayer, null)) ||
            PlayerManager.instance.spawnedPlayer.isHiding) return;
        stepDone = true;

        tutController._NextStep();
    }
}
