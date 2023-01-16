using UnityEngine;

public class PlayerAIBrain_FindFriend : MonoBehaviour
{
    public PlayerAIController playerAIController;

    [Header("Turn settings")]

    public float turnTime;

    public int turnType = -1;
    // turnType = 0 -> stay
    // turnType = 1 -> move to lost friend position
    // turnType = 2 -> move to Random pos in range in a time

    private float randomRange = 10f;
    public bool friendFound;

    public bool defaultSet;

    private void OnEnable()
    {
        defaultSet = true;
    }

    void Update()
    {
        _Default();

        _CheckTurn();

        _CheckHide();
    }

    void _Default()
    {
        if (defaultSet == false) return;

        defaultSet = false;

        turnTime = 0f;

        turnType = -1;

        friendFound = false;
    }

    void _CheckTurn()
    {
        if (FindFriendMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead)
            return;

        if (turnType == -1)
        {
            _GetTurn();
        }

        if (turnType == 0 || turnType == 1 && friendFound == false || turnType == 2)
        {
            if (turnTime >= 0f)
            {
                turnTime -= Time.deltaTime;

                if (turnTime < 0f)
                {
                    _GetTurn();
                }
            }
        }

        if (turnType == 1 && friendFound)
        {
            playerAIController.aIPath._SetMoveToPosition(FindFriendMissionController.instance.findFriendSafeZone.transform.position);
        }
    }

    void _GetTurn()
    {
        int ranNum = Random.Range(0, 100);

        if(ranNum < 40)
        {
            turnType = 0;
        }
        else if(ranNum >= 40 && ranNum < 80)
        {
            turnType = 2;
        }
        else
        {
            turnType = 1;
        }

        if (turnType == 0)
        {
            turnTime = Random.Range(1, 3);

            playerAIController.aIPath._SetMoveToPosition(playerAIController.transform.position);

            //Debug.Log("Player AI Stay");
        }
        else if (turnType == 1)
        {
            turnTime = Random.Range(1, 5);

            playerAIController.aIPath._SetMoveToPosition(FindFriendMissionController.instance.spawnedFriend.transform.position);
        }
        else if (turnType == 2)
        {
            turnTime = Random.Range(3, 5);

            Vector3 ranPos = new Vector3(Random.Range(-randomRange, randomRange), playerAIController.transform.position.y, Random.Range(-randomRange, randomRange));

            ranPos = transform.position + ranPos;

            //var info = AstarPath.active.GetNearest(ranPos);
            //var node = info.node;
            //Vector3 closestPoint = info.position;

            //Debug.Log("ranPos = " + ranPos);

            playerAIController.aIPath._SetMoveToPosition(ranPos);

            //Debug.Log("Player AI move to random position");
        }
    }

    float hideDelay;
    void _CheckHide()
    {
        if (FindFriendMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead)
            return;

        if (hideDelay >= 0f)
        {
            hideDelay -= Time.deltaTime;

            if (hideDelay < 0f)
            {
                if (playerAIController.isHiding)
                {
                    hideDelay = Random.Range(5, 15);
                }
                else
                {
                    hideDelay = Random.Range(5, 10);
                }

                playerAIController._SetHide();
            }
        }
    }
}
