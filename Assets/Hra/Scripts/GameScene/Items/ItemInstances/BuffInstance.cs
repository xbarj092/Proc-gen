using AYellowpaper.SerializedCollections;

public class BuffInstance : ItemInstance
{
    public SerializedDictionary<SpecialBuffEffect, float> SpecialBuffEffects;

    public BuffInstance(BuffItem item) : base(item)
    {
        SpecialBuffEffects = item.SpecialBuffEffects;
    }
}
