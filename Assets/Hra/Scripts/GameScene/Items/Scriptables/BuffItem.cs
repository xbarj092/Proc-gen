using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New buff", menuName = "Item/Buff")]
public class BuffItem : ItemBase
{
    public SerializedDictionary<SpecialBuffEffect, float> SpecialBuffEffects;
}
