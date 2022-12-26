using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsBonus : MonoBehaviour
{
    public WinUI winUI;

    [SerializeField] private int bonusNum;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Pointer")
        {
            winUI._SetBonusMulti(bonusNum);
        }

    }
}
