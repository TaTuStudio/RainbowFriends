using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterUI : MonoBehaviour
{
    public void _OpenButton()
    {
        gameObject.SetActive(true);
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
        foreach(ReuseGO m in PlayerManager.instance.spawnedMonsters)
        {
            MonsterController monsterController = m.GetComponent<MonsterController>();

            if(monsterController != null)
            {
                monsterController.outlinable.enabled = true;
            }

            ImpostorMonsterController impostorMonsterController = m.GetComponent<ImpostorMonsterController>();

            if (impostorMonsterController != null)
            {
                impostorMonsterController.outlinable.enabled = true;
            }
        }

        _CloseButton();
    }
}
