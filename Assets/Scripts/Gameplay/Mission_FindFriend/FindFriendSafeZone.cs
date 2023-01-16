using UnityEngine;

public class FindFriendSafeZone : MonoBehaviour
{
    public PlayerAIController friendFound;

    private void OnTriggerEnter(Collider other)
    {
        PlayerAIController playerAIController = other.GetComponent<PlayerAIController>();

        if(playerAIController != null && FindFriendMissionController.instance != null && FindFriendMissionController.instance.spawnedFriend != null && playerAIController == FindFriendMissionController.instance.spawnedFriend)
        {
            friendFound = playerAIController;
        }
    }
}
