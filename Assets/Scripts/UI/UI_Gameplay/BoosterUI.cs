using System.Runtime.InteropServices;
using UnityEngine;

public class BoosterUI : MonoBehaviour
{
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

        _SpeedUpButtonDone();
    }

    public void _SpeedUpButtonDone()
    {
        PlayerManager.instance.spawnedPlayer.boostSpeed = 0.2f * PlayerController.maxSpeed;

        _CloseButton();
    }

    public void _More20SecondButton()
    {
        Debug.Log("+20 seconds");

        _More20SecondButtonDone();
    }

    public void _More20SecondButtonDone()
    {
        GameController.instance._SetGameTime(GameController.instance.curGameTime + 20f);

        _CloseButton();
    }

    public void _SeeBossButton()
    {
        Debug.Log("See boss");

        _SeeBossButtonDone();
    }

    public void _SeeBossButtonDone()
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

        _CloseButton();
    }
}
