using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellectGameUIButton : MonoBehaviour
{
    public int indexNum = -1;

    public string mapSceneName = "";
    public string mapName = "";

    public bool unlocked = false;

    public TextMeshProUGUI mapNameText;
    public Transform lockImg;

    public bool set = false;

    public int archivementIndex = -1;
    public int winCount = 0;
    public TextMeshProUGUI winCountText;
    public int loseCount = 0;
    public TextMeshProUGUI loseCountText;

    private void Start()
    {
        archivementIndex = -1;
        winCount = 0;
        loseCount = 0;
    }

    private void OnEnable()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(_Sellect);
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

            _GetArchivements();
        }
        else
        {
            unlocked = false;

            lockImg.gameObject.SetActive(true);
        }

        mapNameText.text = mapName;
    }

    void _GetArchivements()
    {
        if(archivementIndex < 0)
        {
            for (int i= 0; i< PlayerStats.instance.mapArchivements.Count; i++ )
            {
                if (PlayerStats.instance.mapArchivements[i].mapSceneName == mapSceneName)
                {
                    archivementIndex = i;

                    break;
                }
            }
        }

        if(archivementIndex >= 0)
        {
            PlayerStats.MapArchivements mapArchivements = PlayerStats.instance.mapArchivements[archivementIndex];

            winCount = mapArchivements.winCount;
            loseCount = mapArchivements.loseCount;
        }

        winCountText.text = "Survived: " + winCount;

        loseCountText.text = "Death: " + loseCount;
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
