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
}
