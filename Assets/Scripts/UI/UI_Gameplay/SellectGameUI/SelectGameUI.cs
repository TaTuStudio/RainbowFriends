using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectGameUI : MonoBehaviour
{
    [SerializeField]
    private PlayerStats stats;

    public SellectGameUIButton[] sellectGameUIButtons;

    public void _PlayButton(string sceneName)
    {
        MapManager.instance.selectedMap = sceneName;

        MapManager.instance._SpawnMap();
    }

    public bool _CheckCanBeUnlocked(int mapIndex)
    {
        //if(mapIndex == 1 && sellectGameUIButtons[0].winCount >= 5)
        //{
        //    return true;
        //}
        //else if (mapIndex == 2 && sellectGameUIButtons[1].loseCount >= 10)
        //{
        //    return true;
        //}
        //else if (mapIndex == 3 && sellectGameUIButtons[0].winCount >= 10 && sellectGameUIButtons[2].loseCount >= 5)
        //{
        //    return true;
        //}
        //else if (mapIndex == 4 && sellectGameUIButtons[2].winCount >= 10 && sellectGameUIButtons[3].loseCount >= 5)
        //{
        //    return true;
        //}

        if(PlayerStats.instance.coin >= 10000 && PlayerStats.instance.mapUnlockedList.Contains(mapIndex) == false)
        {
            return true;
        }

        return false;
    }

    public void _UnlockMap(int unlockMapIndex)
    {
        if (PlayerStats.instance.mapUnlockedList.Contains(unlockMapIndex) == false)
        {
            PlayerStats.instance.mapUnlockedList.Add(unlockMapIndex);
        }
    }

    public string _GetUnlockText(int mapIndex)
    {
        //if (mapIndex == 1)
        //{
        //    return "Survive \"" + sellectGameUIButtons[0].mapName + "\" 5 times.";
        //}
        //else if (mapIndex == 2)
        //{
        //    return "Death to \"" + sellectGameUIButtons[1].mapName + "\" 10 times.";
        //}
        //else if (mapIndex == 3)
        //{
        //    return "Survive \"" + sellectGameUIButtons[0].mapName + "\" 10 times." + "\n" + "Death to \"" + sellectGameUIButtons[2].mapName + "\" 5 times.";
        //}
        //else if (mapIndex == 4)
        //{
        //    return "Survive \"" + sellectGameUIButtons[2].mapName + "\" 10 times." + "\n" + "Death to \"" + sellectGameUIButtons[3].mapName + "\" 5 times.";
        //}

        return "You need 1000 Coin to unlock this map. \n Do you want to unlock it?";
    }

#if UNITY_EDITOR

    private void OnValidate()
    {
        for (int i = 0; i < sellectGameUIButtons.Length; i++)
        {
            sellectGameUIButtons[i].indexNum = i;
        }
    }

#endif
}
