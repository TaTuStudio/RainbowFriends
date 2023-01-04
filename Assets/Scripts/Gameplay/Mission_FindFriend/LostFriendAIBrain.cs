using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostFriendAIBrain : MonoBehaviour
{
    public PlayerAIController playerAIController;

    float distToFollow = 3f;

    public PlayerController followPlayerController;

    public PlayerAIController followPlayerAIController;

    public bool defaultSet = false;

    private void OnEnable()
    {
        defaultSet = true;
    }

    private void Update()
    {
        _Default();

        _CheckIsFound();

        _Follow();

        _CheckHide();
    }

    void _Default()
    {
        if (defaultSet == false) return;

        defaultSet = false;

        followPlayerController = null;
        followPlayerAIController = null;
    }

    void _CheckIsFound()
    {
        if(FindFriendMissionController.instance.gameplaySet == false || PlayerManager.instance.spawnedPlayer == null || followPlayerController != null || followPlayerAIController != null || playerAIController == null)
        {
            return;
        }

        float dist = Vector3.Distance(PlayerManager.instance.spawnedPlayer.transform.position, playerAIController.transform.position);

        if(dist < distToFollow)
        {
            followPlayerController = PlayerManager.instance.spawnedPlayer;

            return;
        }

        foreach(PlayerAIBrain_FindFriend brain in FindFriendMissionController.instance.playerAIBrain_FindFriend_Controller.playerAIBrains_FindFriend)
        {
            dist = Vector3.Distance(brain.playerAIController.transform.position, playerAIController.transform.position);

            if (dist < distToFollow)
            {
                brain.friendFound = true;

                followPlayerAIController = brain.playerAIController;

                return;
            }

        }
    }

    void _Follow()
    {
        if (FindFriendMissionController.instance.gameplaySet == false || playerAIController == null)
        {
            return;
        }

        if (followPlayerController != null)
        {
            playerAIController.aIPath._SetMoveToPosition(followPlayerController.transform.position);

            if (followPlayerController.isDead)
            {
                followPlayerController = null;
            }
        }
        else if (followPlayerAIController != null)
        {
            playerAIController.aIPath._SetMoveToPosition(followPlayerAIController.transform.position);

            if (playerAIController.isDead)
            {
                playerAIController = null;
            }
        }
    }

    float hideDelay = 0f;
    void _CheckHide()
    {
        if (FindFriendMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead || followPlayerController == null && followPlayerAIController == null)
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
