using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutTimeUI : MonoBehaviour
{
    int rewardCoin = 100;

    public TextMeshProUGUI coinText;

    public SoundEffectSO failSound;

    private void OnEnable()
    {
        Time.timeScale = 0f;        
        failSound.Play();

        coinText.text = "" + rewardCoin;
        coinText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public void _MoreTimeAd()
    {
        Debug.Log("More time ad");

        _MoreTimeAdDone();
    }

    public void _MoreTimeAdDone()
    {
        GameController.instance.curGameTime += 30f;

        GameController.instance.resetWinLose = true;

        gameObject.SetActive(false);
    }

    public void _PlayAgain()
    {
        Debug.Log("OutTimeUI Play again");

        GameController.instance._AddLoseArchivement();

        PlayerStats.instance._AddCoin(rewardCoin);

        MapManager.instance._SpawnMap();
    }
}
