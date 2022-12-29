using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class OutTimeUI : MonoBehaviour
{
    public GameObject bonusButton;
    
    private Tweener bonusTween;
    private void OnEnable()
    {
        Time.timeScale = 0f;
        bonusTween = bonusButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);
    }

    private void OnDisable()
    {
        bonusTween.Pause();
    }

    public void _MoreTimeAd()
    {
        Debug.Log("More time ad");

        _MoreTimeAdDone();
    }

    public void _MoreTimeAdDone()
    {
        Time.timeScale = 1f;

        GameController.instance.curGameTime += 30f;

        GameController.instance.resetWinLose = true;

        gameObject.SetActive(false);
    }

    public void _PlayAgain()
    {
        Debug.Log("OutTimeUI Play again");

        GameController.instance._AddLoseArchivement();

        Time.timeScale = 1f;

        MapManager.instance._SpawnMap();
    }
}
