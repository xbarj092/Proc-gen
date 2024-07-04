using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private LevellingData _levellingData;
    public LevellingData LevellingData
    {
        get => _levellingData;
        set 
        { 
            _levellingData = value;
            DataEvents.OnLevellingDataChangedInvoke(_levellingData);
        }
    }
}
