using AYellowpaper.SerializedCollections;
using System;

[Serializable]
public class NonStackableArmorInstance : NonStackableItemInstance
{
    public SerializedDictionary<SpecialArmorEffect, float> SpecialArmorEffects;
    public int Defense;

    public NonStackableArmorInstance(string friendlyID, RarityType rarity, int instanceID, SerializedDictionary<SpecialArmorEffect, float> specialArmorEffects, int defense) : 
        base(friendlyID, rarity, instanceID)
    {
        SpecialArmorEffects = specialArmorEffects;
        Defense = defense;
    }
}
