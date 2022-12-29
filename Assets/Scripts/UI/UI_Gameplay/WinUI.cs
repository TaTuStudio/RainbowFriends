using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class WinUI : MonoBehaviour
{
    int rewardCoin = 500;

    [SerializeField]
    int bonusCoin = 0;
    [SerializeField]
    int bonusMulti = 0;

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

        bonusButton.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine);

        pointerTween.DOPlay();

        noThanksText.color = new Color32(255, 255, 255, 0);

        noThanksText.DOFade(1, 3f).SetDelay(3f);

        GameController.instance._AddWinArchivement();
    }

    private void OnDisable()
    {
        pointerTween.DOKill();
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

        Debug.Log("Win Bonus ad button");

        _BonusAdButtonDone();
    }

    void _BonusAdButtonDone()
    {
        rewardCoinText.text = "" + (bonusCoin);
        rewardCoinText.GetComponent<ContentSizeFitter>().SetLayoutHorizontal();

        bonusButton.SetActive(false);
    }

    public void _HomeButton()
    {
        Debug.Log("Home button");

        for (int i = 0; i < GameplayUI.instance.selectGameUI.sellectGameUIButtons.Length; i++)
        {
            if (PlayerStats.instance.mapUnlockedList.Contains(i) == false)
            {
                PlayerStats.instance.mapUnlockedList.Add(i);

                break;
            }
        }

        PlayerStats.instance._AddCoin(bonusCoin);

        GameController.instance._GameplayBackToHome();
    }

}
