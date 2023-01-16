using System.Runtime.InteropServices;
using UnityEngine;

public class LostFriendAIBrain : MonoBehaviour
{
    public PlayerAIController playerAIController;

    float distToFollow = 3f;

    public PlayerController followPlayerController;

    public PlayerAIController followPlayerAIController;

    public bool defaultSet;

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
        if(FindFriendMissionController.instance.gameplaySet == false || ReferenceEquals(PlayerManager.instance.spawnedPlayer , null) || !ReferenceEquals(followPlayerController , null) || !ReferenceEquals(followPlayerAIController , null) || ReferenceEquals(playerAIController , null))
        {
            return;
        }

        float dist = Vector3.Distance(PlayerManager.instance.spawnedPlayer.transform.position, playerAIController.transform.position);

        if(dist < distToFollow)
        {
            followPlayerController = PlayerManager.instance.spawnedPlayer;

            return;
        }

        foreach(PlayerAIBrain_FindFriend brain in CollectionMarshal.AsSpan(FindFriendMissionController.instance
                    .playerAIBrain_FindFriend_Controller.playerAIBrains_FindFriend))
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
        if (FindFriendMissionController.instance.gameplaySet == false || ReferenceEquals(playerAIController , null))
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

    float hideDelay;
    void _CheckHide()
    {
        if (FindFriendMissionController.instance.gameplaySet == false || playerAIController.catched || playerAIController.isDead ||
            ReferenceEquals(followPlayerController , null) && ReferenceEquals(followPlayerAIController , null))
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
