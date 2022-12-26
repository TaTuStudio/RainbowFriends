using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameUI : MonoBehaviour
{
    public SellectGameUIButton[] sellectGameUIButtons; 

    public void _PlayButton(string sceneName)
    {
        MapManager.instance.selectedMap = sceneName;

        MapManager.instance._SpawnMap();
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        for(int i=0; i< sellectGameUIButtons.Length; i++)
        {
            sellectGameUIButtons[i].indexNum = i;
        }
    }

#endif
}
