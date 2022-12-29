using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutTimeUI : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
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
