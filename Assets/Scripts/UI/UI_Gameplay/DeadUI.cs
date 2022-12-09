using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadUI : MonoBehaviour
{
    public void _MoreLifeAd()
    {
        Debug.Log("More life ad");
    }

    public void _PlayAgain()
    {
        Debug.Log("DeadUI Play again");

        MapManager.instance._SpawnMap();
    }
}
