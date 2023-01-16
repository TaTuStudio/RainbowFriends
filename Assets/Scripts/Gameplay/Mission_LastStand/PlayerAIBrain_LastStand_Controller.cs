using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAIBrain_LastStand_Controller : MonoBehaviour
{
    public List<PlayerAIBrain_LastStand> playerAIBrains_LastStands = new List<PlayerAIBrain_LastStand>();

    public void _SetBrainToAIPlayer()
    {
        List<PlayerAIController> tempList = new List<PlayerAIController>();

        tempList.AddRange(PlayerManager.instance.spawnedAIPlayers);

        foreach (PlayerAIBrain_LastStand brain in CollectionMarshal.AsSpan(playerAIBrains_LastStands))
        {
            if (tempList.Count > 0)
            {
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
