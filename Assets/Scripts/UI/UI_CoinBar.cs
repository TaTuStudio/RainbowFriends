using TMPro;
using UnityEngine;

public class UI_CoinBar : MonoBehaviour
{
    int lastCoin = -1;

    public TextMeshProUGUI coinText;

    private void Update()
    {
        if (lastCoin != PlayerStats.instance.coin)
        {
            lastCoin = PlayerStats.instance.coin;

            coinText.text = lastCoin.ToString();
        }
    }
}
