using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    int rewardCoin = 500;

    [SerializeField]
    int bonusCoin;
    [SerializeField]
    int bonusMulti;

    public TextMeshProUGUI rewardCoinText;

    public TextMeshProUGUI bonusCoinText;

    public GameObject bonusButton;

    public DOTweenAnimation pointerTween;

    public TextMeshProUGUI noThanksText;

    private void OnEnable()
    {
        bonusCoin = 0;
        bonusMulti = 0;

        rewardCoinText.text = rewardCoin.ToString();

        rewardCoinText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();

        bonusButton.SetActive(true);
        
        pointerTween.DOPlay();

        noThanksText.color = new Color32(255, 255, 255, 0);

        noThanksText.DOFade(1, 3f).SetDelay(3f);

        GameController.instance._AddWinArchivement();
        
        SdkManager.Instance.SendFAWinLevel();

        AdsManager.Instance.OnRewarded += _BonusAdButtonDone;

    }

    private void OnDisable()
    {
        pointerTween.DOPause();
        AdsManager.Instance.OnRewarded -= _BonusAdButtonDone;

    }
    

    public void _SetBonusMulti(int num)
    {
        bonusMulti = num;

        bonusCoin = rewardCoin * bonusMulti;

        bonusCoinText.text = bonusCoin.ToString();
    }

    public void _BonusAdButton()
    {
        pointerTween.DOPause();

        AdsManager.Instance.ShowRewardedAd(1);

        //Debug.Log("Win Bonus ad button");
    }

    void _BonusAdButtonDone()
    {
        if (AdsManager.Instance.rewardPos != 1)
            return;
        
        rewardCoinText.text = "" + (bonusCoin);
        rewardCoinText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();

        bonusButton.SetActive(false);

        _HomeButton();
    }

    public void _HomeButton()
    {
        // Debug.Log("Home button");
        AdsManager.Instance.ShowInterstitialAd();

        PlayerStats.instance._AddCoin(bonusCoin);

        GameController.instance._GameplayBackToHome();
    }

}
