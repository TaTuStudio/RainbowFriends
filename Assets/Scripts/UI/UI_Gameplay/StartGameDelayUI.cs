using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartGameDelayUI : MonoBehaviour
{
    float delayTime = 11f;

    [SerializeField]
    float curDelayTime = 0f;

    public MapIntroUI mapIntroUI;

    private void OnEnable()
    {
        curDelayTime = delayTime;

        if (PlayerStats.instance.currentTut >= TutManager.instance.tutControllers.Length)
        {
            mapIntroUI.gameObject.SetActive(true);
        }
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

            //delayText.text = "" + (int)curDelayTime;

            GameplayUI.instance.timeCountUI._SetTime(curDelayTime);

        if (!(curDelayTime <= 0f)) return;
        GameController.instance._SetPlaying(true);

        GameplayUI.instance._ActiveBoosterUI(false);

        gameObject.SetActive(false);
    }
}
