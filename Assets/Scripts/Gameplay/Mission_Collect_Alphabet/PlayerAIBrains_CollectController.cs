using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerAIBrains_CollectController : MonoBehaviour
{
    //public static PlayerAIBrains_CollectController instance;

    public List<PlayerAIBrain_Collect> playerAIBrain_Collects = new List<PlayerAIBrain_Collect>();

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

        foreach (PlayerAIBrain_Collect brain in CollectionMarshal.AsSpan(playerAIBrain_Collects))
        {
            if(tempList.Count > 0)
            {
                brain.playerAIBrains_CollectController = this;
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
