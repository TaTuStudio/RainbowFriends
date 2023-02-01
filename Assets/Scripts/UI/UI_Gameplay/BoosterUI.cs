using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class BoosterUI : MonoBehaviour
{
    private void OnEnable()
    {
        AdsManager.Instance.OnRewarded += CheckBoosterReward;
    }
    private void OnDisable()
    {
        AdsManager.Instance.OnRewarded -= CheckBoosterReward;
    }

    private void CheckBoosterReward()
    {
        switch (AdsManager.Instance.rewardPos)
        {
            case 3:
                _SpeedUpButtonDone();
                break;
            case 4:
                _More20SecondButtonDone();
                break;
            case 5:
                _SeeBossButtonDone();
                break;
        }
    }


    public void _OpenButton()
    {
        if(PlayerStats.instance.currentTut >= TutManager.instance.tutControllers.Length)
        {
            gameObject.SetActive(true);
        }
        else
        {
            _CloseButton();
        }
    }

    public void _CloseButton()
    {
        gameObject.SetActive(false);
    }

    public void _SpeedUpButton()
    {
        Debug.Log("Speed up");
        AdsManager.Instance.ShowRewardedAd(3);
    }

    private void _SpeedUpButtonDone()
    {
        PlayerManager.instance.spawnedPlayer.boostSpeed = 0.2f * PlayerController.maxSpeed;
        
        SdkManager.Instance.SendFAReward("rw_start1");

        _CloseButton();
    }

    public void _More20SecondButton()
    {
        Debug.Log("+20 seconds");
        AdsManager.Instance.ShowRewardedAd(4);
    }

    private void _More20SecondButtonDone()
    {
        GameController.instance._SetGameTime(GameController.instance.curGameTime + 20f);

        SdkManager.Instance.SendFAReward("rw_start2");

        _CloseButton();
    }

    public void _SeeBossButton()
    {
        Debug.Log("See boss");
        AdsManager.Instance.ShowRewardedAd(5);
    }

    private void _SeeBossButtonDone()
    {
        foreach(ReuseGO m in CollectionMarshal.AsSpan(PlayerManager.instance.spawnedMonsters))
        {
            MonsterController monsterController = m.GetComponent<MonsterController>();

            if(!ReferenceEquals(monsterController,null))
            {
                monsterController.outlinable.enabled = true;
            }

            ImpostorMonsterController impostorMonsterController = m.GetComponent<ImpostorMonsterController>();

            if (!ReferenceEquals(impostorMonsterController,null))
            {
                impostorMonsterController.outlinable.enabled = true;
            }
        }

        SdkManager.Instance.SendFAReward("rw_start3");

        _CloseButton();
    }
}
