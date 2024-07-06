using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CatalogItems
{
    public List<ItemBase> CatalogItemList;

    private List<WeaponItem> _existingWeaponItems;
    private List<ArmorItem> _existingArmorItems;
    private List<BuffItem> _existingBuffItems;
    private List<ConsumableItem> _existingConsumableItems;

    private const string RESOURCE_PATH_WEAPON = "Items/Weapons";
    private const string RESOURCE_PATH_ARMOR = "Items/Armor";
    private const string RESOURCE_PATH_BUFFS = "Items/Buffs";
    private const string RESOURCE_PATH_CONSUMABLES = "Items/Consumables";

    public void InitializeCatalog()
    {
        _existingWeaponItems = Resources.LoadAll<WeaponItem>(RESOURCE_PATH_WEAPON).ToList();
        _existingArmorItems = Resources.LoadAll<ArmorItem>(RESOURCE_PATH_ARMOR).ToList();
        _existingBuffItems = Resources.LoadAll<BuffItem>(RESOURCE_PATH_BUFFS).ToList();
        _existingConsumableItems = Resources.LoadAll<ConsumableItem>(RESOURCE_PATH_CONSUMABLES).ToList();

        CatalogItemList.AddRange(_existingWeaponItems);
        CatalogItemList.AddRange(_existingArmorItems);
        CatalogItemList.AddRange(_existingBuffItems);
        CatalogItemList.AddRange(_existingConsumableItems);
    }
}
