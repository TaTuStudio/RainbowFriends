using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private PlayerStats coinEvent;
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        text.text = coinEvent.coin.ToString();

        coinEvent.OnCoinChange += ChangeCoin;
    }

    private void OnDisable()
    {
        coinEvent.OnCoinChange -= ChangeCoin;
    }

    private void ChangeCoin(int coin)
    {
        text.text = coin.ToString();
    }
}
