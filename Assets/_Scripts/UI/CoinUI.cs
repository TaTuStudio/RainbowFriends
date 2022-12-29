using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        text.text = PlayerStats.instance.coin.ToString();

        PlayerStats.instance.OnCoinChange += ChangeCoin;
    }

    private void OnDisable()
    {
        PlayerStats.instance.OnCoinChange -= ChangeCoin;
    }

    private void ChangeCoin(int coin)
    {
        text.text = coin.ToString();
    }
}
