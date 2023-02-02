using DG.Tweening;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;
    
    public TimeCountUI timeCountUI;
    public PlayerCountUI playerCountUI;
    public CollectCountUI alphabetCollectCountUI;
    public CollectCountUI foodCollectCountUI;
    public CollectCountUI keyCollectCountUI;

    public InGameUI inGameUI;
    public WinUI winUI;
    public OutTimeUI outTimeUI;
    public DeadUI deadUI;
    public StartGameDelayUI startGameDelayUI;
    public BoosterUI boosterUI;

    public SelectGameUI selectGameUI;
    public UnlockMapInfoUI unlockMapInfoUI;

    public ShopUI shopUI;

    public SettingsUI settingsUI;

    public Transform flashLightUI;

    private int screenSize;
    private void Awake()
    {
        instance = this;
        screenSize = Camera.main.aspect > 2.5f ? 3000 : 2500;
    }

    public void _GameplayAlphabetCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(true);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
        SdkManager.Instance.SendFAStartLevel("mode_1");
    }

    public void _GameplayFoodCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(true);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
        SdkManager.Instance.SendFAStartLevel("mode_3");
    }

    public void _GameplayKeyCollectSetup()
    {
        _ActiveTimeCountUI(true);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(true);

        _GameplayUISetup();
        SdkManager.Instance.SendFAStartLevel("mode_4");
    }

    public void _GameplayFindFriendSetup()
    {
        _ActiveTimeCountUI(true);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
        SdkManager.Instance.SendFAStartLevel("mode_2");
    }

    public void _GameplayLastStandSetup()
    {
        _ActiveTimeCountUI(true);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _GameplayUISetup();
        SdkManager.Instance.SendFAStartLevel("mode_5");
    }

    public void _GameplayUISetup()
    {
        _ActiveInGameUI(true);

        _ActiveSelectGameUI(false);
        _ActiveSelectShopUI(false);
    }

    public void _HomeUISetup()
    {
        _ActiveTimeCountUI(false);
        _ActivePlayerCountUI(true);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _ActiveInGameUI(false);
        _ActiveSettings(false);

        _ActiveSelectGameUI(true);
        _ActiveSelectShopUI(false);
    }

    

    public void _SettingsUISetup()
    {
        _ActiveTimeCountUI(false);
        _ActivePlayerCountUI(false);
        _ActiveAlphabetCollectCountUI(false);
        _ActiveFoodCollectCountUI(false);
        _ActiveKeyCollectCountUI(false);

        _ActiveInGameUI(false);

        _ActiveSelectGameUI(true);
        _ActiveSelectShopUI(false);
        
        _ActiveSettings(true);
    }

    private void _ActiveSettings(bool active)
    {
        //settingsUI.gameObject.SetActive(active);
        if (active)
        {
            settingsUI.dimBg.color = new Color(0, 0, 0, 0);
            settingsUI.GetComponent<RectTransform>().DOAnchorPosX(0, 1f)
                .OnComplete(() => settingsUI.dimBg.DOFade(.8f, .5f));
        }
        else
        {
            settingsUI.dimBg.DOFade(0, .5f);
            settingsUI.GetComponent<RectTransform>().DOAnchorPosX(-screenSize, 1f).SetDelay(.2f);
        }
    }
    
    public void _ActiveTimeCountUI(bool active)
    {
        timeCountUI.gameObject.SetActive(active);
    }

    public void _ActivePlayerCountUI(bool active)
    {
        playerCountUI.gameObject.SetActive(active);
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

    public void _ActiveInGameUI(bool active)
    {
        inGameUI.gameObject.SetActive(active);

        _ActiveStartGameDelayUI(active);

        _ActiveBoosterUI(active);

        _ActiveWinUI(false);
        _ActiveOutTimeUI(false);
        _ActiveDeadUI(false);
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
    public void _ActiveStartGameDelayUI(bool active)
    {
        startGameDelayUI.gameObject.SetActive(active);
    }
    public void _ActiveBoosterUI(bool active)
    {
        if (active)
        {
            boosterUI._OpenButton();
        }
        else
        {
            boosterUI._CloseButton();
        }

    }

    public void _ActiveSelectGameUI(bool active)
    {
        selectGameUI.gameObject.SetActive(active);

        unlockMapInfoUI._Close();
    }

    public void _ActiveSelectShopUI(bool active)
    {
        if (active)
        {
            shopUI.lockBG.color = new Color(0, 0, 0, 0);
            shopUI.GetComponent<RectTransform>().DOAnchorPosX(0, 1f)
                .OnComplete(() => shopUI.lockBG.DOFade(.8f, .5f));
        }
        else
        {
            shopUI.lockBG.DOFade(0, .5f);
            shopUI.GetComponent<RectTransform>().DOAnchorPosX(screenSize, 1f).SetDelay(.2f);
        }
    }

    public void _ActiveFlashLight(bool active)
    {
        flashLightUI.gameObject.SetActive(active);
    }
}
