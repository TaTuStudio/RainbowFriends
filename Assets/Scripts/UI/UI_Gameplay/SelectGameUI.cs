using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectGameUI : MonoBehaviour
{
    string selected = "";

    private void OnEnable()
    {
        selected = "";
    }

    public void _PlayButton(string sceneName)
    {
        MapManager.instance.selectedMap = sceneName;

        MapManager.instance._SpawnMap();
    }
}
