using AYellowpaper.SerializedCollections;

public class ConsumableInstance : ItemInstance
{
    public SerializedDictionary<SpecialConsumableEffect, float> SpecialConsumableEffects;
    public float Duration;

    public ConsumableInstance(ConsumableItem item) : base(item)
    {
        SpecialConsumableEffects = item.SpecialConsumableEffects;
        Duration = item.Duration;
    }
}
