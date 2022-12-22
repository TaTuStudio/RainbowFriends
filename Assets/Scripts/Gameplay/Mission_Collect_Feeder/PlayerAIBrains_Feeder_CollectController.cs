using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIBrains_Feeder_CollectController : MonoBehaviour
{
    //public static PlayerAIBrains_CollectController instance;

    public List<PlayerAIBrain_Feeder_Collect> playerAIBrain_Collects = new List<PlayerAIBrain_Feeder_Collect>();

    //private void Awake()
    //{
    //    _MakeReplaceSingleton();
    //}

    //void _MakeReplaceSingleton()
    //{
    //    if (PlayerAIBrains_CollectController.instance != null && PlayerAIBrains_CollectController.instance != this)
    //    {
    //        PlayerAIBrains_CollectController old = PlayerAIBrains_CollectController.instance;

    //        PlayerAIBrains_CollectController.instance = this;

    //        Destroy(old);
    //    }
    //    else
    //    {
    //        PlayerAIBrains_CollectController.instance = this;
    //    }
    //}

    public void _SetBrainToAIPlayer()
    {
        List<PlayerAIController> tempList = new List<PlayerAIController>();

        tempList.AddRange(PlayerManager.instance.spawnedAIPlayers);

        foreach (PlayerAIBrain_Feeder_Collect brain in playerAIBrain_Collects)
        {
            if (tempList.Count > 0)
            {
                brain.playerAIBrains_Feeder_CollectController = this;
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
