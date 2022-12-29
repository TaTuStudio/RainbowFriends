using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Pathfinding;

public class DeadUI : MonoBehaviour
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

    public void _MoreLifeAd()
    {
        Debug.Log("More life ad");

        _MoreLifeAdDone();
    }

    public void _MoreLifeAdDone()
    {
        Time.timeScale = 1f;

        PlayerController spawnedPlayer = PlayerManager.instance.spawnedPlayer;

        spawnedPlayer.setDefault = true;

        spawnedPlayer._Default();

        spawnedPlayer._SetNoDam(5f);

        GameController.instance.resetWinLose = true;

        gameObject.SetActive(false);
    }

    public void _PlayAgain()
    {
        Debug.Log("DeadUI Play again");

        GameController.instance._AddLoseArchivement();

        MapManager.instance._SpawnMap();
    }
}
