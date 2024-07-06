using AYellowpaper.SerializedCollections;
using System;

[Serializable]
public class NonStackableArmorInstance : NonStackableItemInstance
{
    public SerializedDictionary<SpecialArmorEffect, float> SpecialArmorEffects;
    public int Defense;

    public NonStackableArmorInstance(string friendlyID, RarityType rarity, SerializedDictionary<SpecialArmorEffect, float> specialArmorEffects, int defense) : 
        base(friendlyID, rarity)
    {
        SpecialArmorEffects = specialArmorEffects;
        Defense = defense;
    }
}
