using System;
using UnityEngine;

[Serializable]
public class LevellingData
{
    [field: SerializeField] public int CurrentLevel { get; private set; }
    [field: SerializeField] public int CurrentXP { get; private set; }
    [field: SerializeField] public int NextLevelXP { get; private set; }
    [field: SerializeField] public int XPToNextLevel { get; private set; }
    [field: SerializeField] public int TotalXP { get; private set; }

    public LevellingData(int currentLevel, int currentXP, int nextLevelXP, int xPToNextLevel, int totalXP)
    {
        CurrentLevel = currentLevel;
        CurrentXP = currentXP;
        NextLevelXP = nextLevelXP;
        XPToNextLevel = xPToNextLevel;
        TotalXP = totalXP;
    }
}
