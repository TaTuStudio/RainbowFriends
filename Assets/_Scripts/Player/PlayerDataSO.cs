using System;
using UnityEngine;
using Lowscope.Saving;
[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Player Data")]
public class PlayerDataSO : ScriptableObject
{
    [SerializeField] private int coin;
    [SerializeField] private int openedLevel;

    public int OpenedLevel
    {
        get => openedLevel;
        set
        {
            openedLevel = value;
            OnOpenChange?.Invoke(openedLevel);
            SaveMaster.SetInt("openedLevel", openedLevel);
        }
    }
    
    public int Coin
    {
        get => coin;
        set
        {
            coin = value;
            OnCoinChange?.Invoke(coin);
            SaveMaster.SetInt("coin", coin);
        }
    }

    public event Action<int> OnCoinChange;
    public event Action<int> OnOpenChange;

    private void OnEnable()
    {
        coin = SaveMaster.GetInt("coin",0);
        openedLevel = SaveMaster.GetInt("openedLevel",0);
    }
}