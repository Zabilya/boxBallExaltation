using System;
using UnityEngine;

[Serializable]
public class GameSave
{
    [SerializeField] private int _coinsCount;
    [SerializeField] private int _currentLevel;
    
    public int CoinsCount
    {
        get => _coinsCount;
        set => _coinsCount = value < 0 ? 0 : value;
    }

    public int CurrentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value < _currentLevel ? _currentLevel : value;
    }
    

    public GameSave()
    {
        CoinsCount = 0;
        CurrentLevel = 1;
    }
    
    public GameSave(int coinsCount, int currentLevel)
    {
        CoinsCount = coinsCount;
        CurrentLevel = currentLevel;
    }
}