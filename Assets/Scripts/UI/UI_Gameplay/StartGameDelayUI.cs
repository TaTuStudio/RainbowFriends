using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGameDelayUI : MonoBehaviour
{
    float delayTime = 9f;

    [SerializeField]
    float curDelayTime = 0f;

    public TextMeshProUGUI delayText;

    private void OnEnable()
    {
        curDelayTime = delayTime;
    }

    // Update is called once per frame
    void Update()
    {
        _Delay();
    }

    public float _GetDelayTime()
    {
        return delayTime;
    }

    void _Delay()
    {
        if (GameController.instance.isPlaying != false || !GameController.instance.gameplaySetupDone ||
            !(curDelayTime > 0f)) return;
        curDelayTime -= Time.deltaTime;

        delayText.text = "" + (int)curDelayTime;

        if (!(curDelayTime <= 0f)) return;
        GameController.instance._SetPlaying(true);

        GameplayUI.instance._ActiveBoosterUI(false);

        gameObject.SetActive(false);
    }
}
