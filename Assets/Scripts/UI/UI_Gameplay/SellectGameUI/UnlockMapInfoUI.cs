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

    private void Update()
    {
        _AutoDeactive();
    }

    void _AutoDeactive()
    {
        if(curDeactiveDelay > 0f)
        {
            curDeactiveDelay -= Time.deltaTime;

            if(curDeactiveDelay <= 0f)
            {
                _Close();
            }
        }
    }

    public void _Setup(string contentStr)
    {
        gameObject.SetActive(true);

        contentText.text = "";

        PlayTweenText(contentStr);
    }

    private void PlayTweenText(string contentStr)
    {
        if (contentStr == "")
        {
            gameObject.SetActive(false);

            return;
        }

        string text = "";

        Tween customTween = DOTween.To(() => text, x => text = x, contentStr, contentStr.Length / speed).OnUpdate(() =>
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
}
