using System;
using UnityEngine;

[Serializable]
public class GameData
{
    [SerializeField] private MapTransformData _mapTransformData;
    public MapTransformData MapTransformData
    {
        get => _mapTransformData;
        set
        {
            _mapTransformData = value;
            GameDataEvents.OnMapTransformDataChangedInvoke(_mapTransformData);
        }
    }

    [SerializeField] private LevellingMultipliers _levellingMultipliers;
    public LevellingMultipliers LevellingMultipliers => _levellingMultipliers;
}
