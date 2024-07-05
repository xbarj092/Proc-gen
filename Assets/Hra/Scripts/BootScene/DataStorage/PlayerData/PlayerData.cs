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
            PlayerDataEvents.OnLevellingDataChangedInvoke(_levellingData);
        }
    }

    [SerializeField] private TransformData _transformData;
    public TransformData TransformData
    {
        get => _transformData;
        set
        {
            _transformData = value;
            PlayerDataEvents.OnTransformDataChangedInvoke(_transformData);
        }
    }
}
