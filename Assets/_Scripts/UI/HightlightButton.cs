using System;
using DG.Tweening;
using UnityEngine;

public class HightlightButton : MonoBehaviour
{
    private Tweener highlighTweener;
    private void OnEnable()
    {
        if (highlighTweener == null)
        {
            highlighTweener = gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.OutSine).SetUpdate(true);
        }
        else
        {
            highlighTweener.Restart();
        }
    }

    private void OnDisable()
    {
        highlighTweener.Pause();
    }
}
