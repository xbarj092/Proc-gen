using System.Collections.Generic;
using System.Linq;

public class InventoryHelper
{
    public void AddItemsToInventory(List<ItemInstance> chosenItems)
    {
        InventoryItems inventoryItems = LocalDataStorage.Instance.PlayerData.InventoryItems;
        foreach (ItemInstance item in chosenItems)
        {
            AddItemToInventory(inventoryItems, item);
        }

        LocalDataStorage.Instance.PlayerData.InventoryItems = inventoryItems;
    }

    private void AddItemToInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        if (IsItemStackable(item))
        {
            AddStackableItemToInventory(inventoryItems, item);
        }
        else
        {
            AddNonStackableItemToInventory(inventoryItems, item);
        }
    }

    private void AddStackableItemToInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        if (inventoryItems.StackableItems.StackableItemSpaces.Any(space => space.FriendlyID == item.FriendlyID))
        {
            inventoryItems.StackableItems.StackableItemSpaces.FirstOrDefault(space => space.FriendlyID == item.FriendlyID).Amount++;
        }
        else
        {
            inventoryItems.StackableItems.StackableItemSpaces.Add(new(item.FriendlyID, 1));
        }
    }

    private void AddNonStackableItemToInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        switch (item)
        {
            case ArmorInstance armorInstance:
                inventoryItems.NonStackableItems.NonStackableArmorInstances.Add(
                    new(armorInstance.FriendlyID, armorInstance.Rarity, armorInstance.SpecialArmorEffects, armorInstance.Defense));
                break;
            case WeaponInstance weaponinstance:
                inventoryItems.NonStackableItems.NonStackableWeaponInstances.Add(
                    new(weaponinstance.FriendlyID, weaponinstance.Rarity, weaponinstance.Damage, weaponinstance.Range, weaponinstance.AttacksPerSecond, weaponinstance.Pierce));
                break;
            case BuffInstance buffInstance:
                inventoryItems.NonStackableItems.NonStackableBuffInstances.Add(
                    new(buffInstance.FriendlyID, buffInstance.Rarity, buffInstance.SpecialBuffEffects));
                break;
        }
    }

    private bool IsItemStackable(ItemInstance item)
    {
        return item is ConsumableInstance;
    }
}
