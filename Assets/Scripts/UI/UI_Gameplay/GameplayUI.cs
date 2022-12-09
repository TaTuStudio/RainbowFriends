using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    public TimeCountUI timeCountUI;
    public CollectCountUI alphabetCollectCountUI;

    public WinUI winUI;
    public OutTimeUI outTimeUI;
    public DeadUI deadUI;

    private void Awake()
    {
        instance = this;
    }

    public void _GameplayAlphabetCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(true);
        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);
    }

    public void _ActiveTimeCountUI(bool active)
    {
        timeCountUI.gameObject.SetActive(active);
    }

    public void _ActiveAlphabetCollectCountUI(bool active)
    {
        alphabetCollectCountUI.gameObject.SetActive(active);
    }

    public void _ActiveWinUI(bool active)
    {
        winUI.gameObject.SetActive(active);
    }
    public void _ActiveOutTimeUI(bool active)
    {
        outTimeUI.gameObject.SetActive(active);
    }
    public void _ActiveDeadUI(bool active)
    {
        deadUI.gameObject.SetActive(active);
    }
}
