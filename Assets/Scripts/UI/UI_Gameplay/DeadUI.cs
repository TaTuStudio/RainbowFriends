using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Pathfinding;

public class DeadUI : MonoBehaviour
{

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
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
