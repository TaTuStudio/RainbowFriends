using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SellectGameUIButton : MonoBehaviour
{
    public int indexNum = -1;

    public string mapSceneName = "";
    public string mapName = "";

    public bool unlocked = false;

    public TextMeshProUGUI mapNameText;
    public Transform lockImg;

    public bool set = false;

    private void OnEnable()
    {
        set = true;
    }

    private void Update()
    {
        if (set)
        {
            set = false;

            _SetActive();
        }
    }

    public void _SetActive()
    {
        if(PlayerStats.instance.mapUnlockedList.Contains(indexNum))
        {
            unlocked = true;

            lockImg.gameObject.SetActive(false);
        }
        else
        {
            unlocked = false;

            lockImg.gameObject.SetActive(true);
        }

        mapNameText.text = mapName;
    }

    public void _Sellect()
    {
        GameplayUI.instance.selectGameUI._PlayButton(mapSceneName);
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if (set)
        {
            set = false;

            mapNameText.text = mapName;

            gameObject.name = mapName;
        }
    }

#endif
}
