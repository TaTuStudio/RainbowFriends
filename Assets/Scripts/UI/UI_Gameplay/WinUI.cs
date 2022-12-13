using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    int rewardCoin = 50;

    public int bonusCoin = 0;

    public int bonusMulti = 0;

    public void _BonusAdButton()
    {
        Debug.Log("Win Bonus ad button");
    }

    public void _HomeButton()
    {
        Debug.Log("Home button");

        GameController.instance._GameplayBackToHome();
    }
}
