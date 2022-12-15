using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerPos", menuName = "Player/New Player Pos")]
public class PlayerPosSO : ScriptableObject
{
    private int pos;
    
    public int Pos
    {
        get => pos;
        set
        {
            pos = value;
            OnPosChange?.Invoke(pos);
        }
    }

    public event Action<int> OnPosChange;
}
