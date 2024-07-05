using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New consumable", menuName = "Item/Consumable")]
public class ConsumableItem : ItemBase
{
    public SerializedDictionary<SpecialConsumableEffect, float> SpecialConsumableEffects;
    public float Duration;
}
