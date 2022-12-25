using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShowAiDiedUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] text;
    private void OnEnable()
    {
        PlayerAIController.OnAIDied += ShowAIDiedText;
    }

    private void OnDisable()
    {
        PlayerAIController.OnAIDied -= ShowAIDiedText;
    }

    private void ShowAIDiedText()
    {
        for (var i = 0; i < text.Length; i++)
        {
            text[i].gameObject.SetActive(true);
            text[i].color = i==0 ? new Color32(255, 255, 255, 0) : new Color32(255, 0, 0, 0);
            text[i].DOFade(1, 0.5f);
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return 1f.NewWaitForSeconds();
        foreach (var _t in text)
        {
            _t.DOFade(0, 0.5f).OnComplete(() => _t.gameObject.SetActive(false));
        }
    }
}