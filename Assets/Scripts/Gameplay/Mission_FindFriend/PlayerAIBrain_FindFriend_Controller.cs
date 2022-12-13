using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIBrain_FindFriend_Controller : MonoBehaviour
{
    public LostFriendAIBrain lostFriendAIBrain;
    
    public List<PlayerAIBrain_FindFriend> playerAIBrains_FindFriend = new List<PlayerAIBrain_FindFriend>();

    public void _SetBrainToAIPlayer()
    {
        lostFriendAIBrain.playerAIController = FindFriendMissionController.instance.spawnedFriend;

        List<PlayerAIController> tempList = new List<PlayerAIController>();

        tempList.AddRange(PlayerManager.instance.spawnedAIPlayers);

        foreach (PlayerAIBrain_FindFriend brain in playerAIBrains_FindFriend)
        {
            if (tempList.Count > 0)
            {
                brain.playerAIBrain_FindFriend_Controller = this;
                brain.playerAIController = tempList[0];

                tempList.RemoveAt(0);
            }
            else
            {
                Debug.Log("Collect ai brain not enough");

                brain.playerAIController = null;
            }
        }
    }
}
