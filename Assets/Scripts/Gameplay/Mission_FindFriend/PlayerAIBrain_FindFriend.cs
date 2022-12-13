using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIBrain_FindFriend : MonoBehaviour
{
    public PlayerAIBrain_FindFriend_Controller playerAIBrain_FindFriend_Controller;

    public PlayerAIController playerAIController;

    [Header("Turn settings")]

    public float turnTime = 0f;

    public int turnType = -1;
    // turnType = 0 -> stay
    // turnType = 1 -> move to lost friend position
    // turnType = 2 -> move to Random pos in range in a time

    public float randomRange = 10f;
    public bool friendFound = false;

    public bool defaultSet = false;

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

        if (turnType == 1 && friendFound == true)
        {
            playerAIController.aIPath._SetMoveToPosition(FindFriendMissionController.instance.findFriendSafeZone.transform.position);
        }
    }

    void _GetTurn()
    {
        List<int> ranNumList = new List<int>() { 0, 1, 2 };

        turnType = Random.Range(0, ranNumList.Count);

        if (turnType == 0)
        {
            turnTime = (float)Random.RandomRange(1, 3);

            playerAIController.aIPath._SetMoveToPosition(playerAIController.transform.position);

            //Debug.Log("Player AI Stay");
        }
        else if (turnType == 1)
        {
            turnTime = (float)Random.RandomRange(3, 5);

            playerAIController.aIPath._SetMoveToPosition(FindFriendMissionController.instance.spawnedFriend.transform.position);
        }
        else if (turnType == 2)
        {
            turnTime = (float)Random.RandomRange(3, 5);

            Vector3 ranPos = new Vector3((float)Random.RandomRange(-randomRange, randomRange), playerAIController.transform.position.y, (float)Random.RandomRange(-randomRange, randomRange));

            ranPos = transform.position + ranPos;

            //var info = AstarPath.active.GetNearest(ranPos);
            //var node = info.node;
            //Vector3 closestPoint = info.position;

            //Debug.Log("ranPos = " + ranPos);

            playerAIController.aIPath._SetMoveToPosition(ranPos);

            //Debug.Log("Player AI move to random position");
        }
    }

    float hideDelay = 0f;
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
                    hideDelay = (float)Random.Range(5, 15);
                }
                else
                {
                    hideDelay = (float)Random.Range(5, 10);
                }

                playerAIController._SetHide();
            }
        }
    }
}
