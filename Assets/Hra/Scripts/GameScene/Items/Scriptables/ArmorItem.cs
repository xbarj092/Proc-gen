using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "New armor", menuName = "Item/Armor")]
public class ArmorItem : ItemBase
{
    public SerializedDictionary<SpecialArmorEffect, float> SpecialArmorEffects;
    public int Defense;
}
