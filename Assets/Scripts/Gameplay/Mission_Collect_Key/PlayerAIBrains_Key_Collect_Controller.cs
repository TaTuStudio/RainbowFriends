using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAIBrains_Key_Collect_Controller : MonoBehaviour
{
    public static PlayerAIBrains_Key_Collect_Controller instance;

    public List<PlayerAIBrain_Key_Collect> playerAIBrain_Collects = new List<PlayerAIBrain_Key_Collect>();

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

        foreach (PlayerAIBrain_Key_Collect brain in playerAIBrain_Collects)
        {
            if (tempList.Count > 0)
            {
                brain.playerAIBrains_key_Collect_Controller = this;
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
