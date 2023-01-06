using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UnlockMapInfoUI : MonoBehaviour
{
    public TextMeshProUGUI contentText;

    float speed = 60f;

    float curDeactiveDelay = 0f;

    int selectMapIndex = -1;

    private void Update()
    {
        //_AutoDeactive();
    }

    void _AutoDeactive()
    {
        if (!(curDeactiveDelay > 0f)) return;
        curDeactiveDelay -= Time.deltaTime;

        if(curDeactiveDelay <= 0f)
        {
            _Close();
        }
    }

    public void _Setup(int mapIndex)
    {
        gameObject.SetActive(true);

        selectMapIndex = mapIndex;

        contentText.text = "";

        PlayTweenText(GameplayUI.instance.selectGameUI._GetUnlockText(selectMapIndex));
    }

    private void PlayTweenText(string contentStr)
    {
        if (contentStr == "")
        {
            gameObject.SetActive(false);

            return;
        }

        string text = "";

        DOTween.To(() => text, x => text = x, contentStr, contentStr.Length / speed).OnUpdate(() =>
        {

            contentText.text = text;

        }).OnComplete(() =>
        {
            curDeactiveDelay = 2f;
        });
    }

    public void _Close()
    {
        gameObject.SetActive(false);
    }

    public void _ConfirmToUnlock()
    {
        if (GameplayUI.instance.selectGameUI._CheckCanBeUnlocked(selectMapIndex) && PlayerStats.instance.coin >= 10000)
        {
            GameplayUI.instance.selectGameUI._UnlockMap(selectMapIndex);

            GameplayUI.instance.selectGameUI.sellectGameUIButtons[selectMapIndex]._SetActive();

            PlayerStats.instance._SubCoin(10000);
        }

        _Close();
    }
}
