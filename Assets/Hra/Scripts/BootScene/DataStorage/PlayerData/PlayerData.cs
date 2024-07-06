using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private PlayerStatistics _playerStatistics;
    public PlayerStatistics PlayerStatistics
    {
        get => _playerStatistics;
        set
        {
            _playerStatistics = value;
            PlayerDataEvents.OnPlayerStatisticsChangedInvoke(_playerStatistics);
        }
    }

    [SerializeField] private LevellingData _levellingData;
    public LevellingData LevellingData
    {
        get => _levellingData;
        set 
        { 
            _levellingData = value;
            PlayerDataEvents.OnLevellingDataChangedInvoke(_levellingData);
        }
    }

    [SerializeField] private TransformData _transformData;
    public TransformData TransformData
    {
        get => _transformData;
        set
        {
            _transformData = value;
            PlayerDataEvents.OnTransformDataChangedInvoke(_transformData);
        }
    }

    [SerializeField] private EquippedItemsData _equippedItemsData;
    public EquippedItemsData EquippedItemsData
    {
        get => _equippedItemsData;
        set
        {
            _equippedItemsData = value;
            PlayerDataEvents.OnEquippedItemsDataChangedInvoke(_equippedItemsData);
        }
    }

    [SerializeField] private SpecialEffectsData _specialEffectsData;
    public SpecialEffectsData SpecialEffectsData
    {
        get => _specialEffectsData;
        set
        {
            _specialEffectsData = value;
            PlayerDataEvents.OnSpecialEffectsDataChangedInvoke(_specialEffectsData);
        }
    }

    [SerializeField] private InventoryItems _inventoryItems;
    public InventoryItems InventoryItems
    {
        get => _inventoryItems;
        set
        {
            _inventoryItems = value;
            PlayerDataEvents.OnInventoryItemsChangedInvoke(_inventoryItems);
        }
    }
}
