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

    public SelectGameUI selectGameUI;

    public ShopUI shopUI;

    public Transform flashLightUI;

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
        _ActiveSelectGameUI(false);
        _ActiveSelectShopUI(false);
    }

    public void _GameplayFindFriendSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);
        _ActiveSelectGameUI(false);
        _ActiveSelectShopUI(false);
    }

    public void _GameplayLastStandSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);
        _ActiveSelectGameUI(false);
        _ActiveSelectShopUI(false);
    }

    public void _HomeUISetup()
    {
        _ActiveTimeCountUI(false);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);
        _ActiveSelectGameUI(true);
        _ActiveSelectShopUI(false);
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

    public void _ActiveSelectGameUI(bool active)
    {
        selectGameUI.gameObject.SetActive(active);
    }

    public void _ActiveSelectShopUI(bool active)
    {
        shopUI.gameObject.SetActive(active);
    }

    public void _ActiveFlashLightUI(bool active)
    {
        flashLightUI.gameObject.SetActive(active);
    }
}
