using AYellowpaper.SerializedCollections;

public class ArmorInstance : ItemInstance
{
    public SerializedDictionary<SpecialArmorEffect, float> SpecialArmorEffects;
    public int Defense;

    public ArmorInstance(ArmorItem item) : base(item)
    {
        SpecialArmorEffects = item.SpecialArmorEffects;
        Defense = item.Defense;

        InstanceID = GenerateInstanceID(LocalDataStorage.Instance.PlayerData.InventoryItems.NonStackableItems.NonStackableArmorInstances);
    }
}
