using AYellowpaper.SerializedCollections;
using System;

[Serializable]
public class NonStackableBuffInstance : NonStackableItemInstance
{
    public SerializedDictionary<SpecialBuffEffect, float> SpecialBuffEffects;

    public NonStackableBuffInstance(string friendlyID, RarityType rarity, SerializedDictionary<SpecialBuffEffect, float> specialBuffEffects) : 
        base(friendlyID, rarity)
    {
        SpecialBuffEffects = specialBuffEffects;
    }
}
