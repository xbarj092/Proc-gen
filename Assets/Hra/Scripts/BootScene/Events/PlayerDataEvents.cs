using System;

public static class PlayerDataEvents
{
    public static event Action<PlayerStatistics> OnPlayerStatisticsChanged;
    public static void OnPlayerStatisticsChangedInvoke(PlayerStatistics playerStatistics)
    {
        OnPlayerStatisticsChanged?.Invoke(playerStatistics);
    }

    public static event Action<LevellingData> OnLevellingDataChanged;
    public static void OnLevellingDataChangedInvoke(LevellingData levellingData)
    {
        OnLevellingDataChanged?.Invoke(levellingData);
    }

    public static event Action<TransformData> OnTransformDataChanged;
    public static void OnTransformDataChangedInvoke(TransformData transformData)
    {
        OnTransformDataChanged?.Invoke(transformData);
    }

    public static event Action<EquippedItemsData> OnEquippedItemsDataChanged;
    public static void OnEquippedItemsDataChangedInvoke(EquippedItemsData equippedItemsData)
    {
        OnEquippedItemsDataChanged?.Invoke(equippedItemsData);
    }

    public static event Action<SpecialEffectsData> OnSpecialEffectsDataChanged;
    public static void OnSpecialEffectsDataChangedInvoke(SpecialEffectsData specialEffectsData)
    {
        OnSpecialEffectsDataChanged?.Invoke(specialEffectsData);
    }

    public static event Action<InventoryItems> OnInventoryItemsChanged;
    public static void OnInventoryItemsChangedInvoke(InventoryItems inventoryItems)
    {
        OnInventoryItemsChanged?.Invoke(inventoryItems);
    }
}
