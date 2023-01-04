using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OutTimeUI : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
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
        
        MapManager.instance._SpawnMap();
    }
}
