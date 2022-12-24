using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;

    public TimeCountUI timeCountUI;
    public CollectCountUI alphabetCollectCountUI;
    public CollectCountUI foodCollectCountUI;
    public CollectCountUI keyCollectCountUI;

    public WinUI winUI;
    public OutTimeUI outTimeUI;
    public DeadUI deadUI;

    public SelectGameUI selectGameUI;

    public ShopUI shopUI;

    public GameObject settingsUI;

    public Transform flashLightUI;

    private void Awake()
    {
        instance = this;
    }

    public void _GameplayAlphabetCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(true);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
    }

    public void _GameplayFoodCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(true);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
    }

    public void _GameplayKeyCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(true);

        _GameplayUISetup();
    }

    public void _GameplayFindFriendSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
    }

    public void _GameplayLastStandSetup()
    {
        _ActiveTimeCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
    }

    public void _GameplayUISetup()
    {
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
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);

        _ActiveSelectGameUI(true);
        _ActiveSelectShopUI(false);
        _ActiveSettingsUI(false);
    }
    public void _SettingsUISetup()
    {
        _ActiveTimeCountUI(false);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);

        _ActiveSelectGameUI(true);
        _ActiveSelectShopUI(false);
        _ActiveSettingsUI(true);
    }


    public void _ActiveTimeCountUI(bool active)
    {
        timeCountUI.gameObject.SetActive(active);
    }

    public void _ActiveAlphabetCollectCountUI(bool active)
    {
        alphabetCollectCountUI.gameObject.SetActive(active);
    }

    public void _ActiveFoodCollectCountUI(bool active)
    {
        foodCollectCountUI.gameObject.SetActive(active);
    }

    public void _ActiveKeyCollectCountUI(bool active)
    {
        keyCollectCountUI.gameObject.SetActive(active);
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

    public void _ActiveFlashLight(bool active)
    {
        flashLightUI.gameObject.SetActive(active);
    }
    public void _ActiveSettingsUI(bool active)
    {
        settingsUI.gameObject.SetActive(active);
    }
}
