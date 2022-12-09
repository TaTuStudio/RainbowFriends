using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTimeUI : MonoBehaviour
{
    public void _MoreTimeAd()
    {
        Debug.Log("More time ad");
    }

    public void _PlayAgain()
    {
        Debug.Log("OutTimeUI Play again");

        MapManager.instance._SpawnMap();
    }
}
