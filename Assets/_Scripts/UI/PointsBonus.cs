using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsBonus : MonoBehaviour
{
    [SerializeField] private int coinBonus;
    [SerializeField] private TextMeshProUGUI textCoinBonus;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        textCoinBonus.text ="+" + coinBonus.ToString();
    }
}
