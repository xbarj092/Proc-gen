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
                    new(armorInstance.FriendlyID, armorInstance.Rarity, item.InstanceID, 
                        armorInstance.SpecialArmorEffects, armorInstance.Defense));
                break;
            case WeaponInstance weaponinstance:
                inventoryItems.NonStackableItems.NonStackableWeaponInstances.Add(
                    new(weaponinstance.FriendlyID, weaponinstance.Rarity, item.InstanceID,
                        weaponinstance.Damage, weaponinstance.Range, weaponinstance.AttacksPerSecond, weaponinstance.Pierce));
                break;
            case BuffInstance buffInstance:
                inventoryItems.NonStackableItems.NonStackableBuffInstances.Add(
                    new(buffInstance.FriendlyID, buffInstance.Rarity, item.InstanceID, 
                        buffInstance.SpecialBuffEffects));
                break;
        }
    }

    public void RemoveItems(List<ItemInstance> items)
    {
        InventoryItems inventoryItems = LocalDataStorage.Instance.PlayerData.InventoryItems;
        foreach (ItemInstance item in items)
        {
            RemoveItemFromInventory(inventoryItems, item);
        }

        LocalDataStorage.Instance.PlayerData.InventoryItems = inventoryItems;
    }

    private void RemoveItemFromInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        if (IsItemStackable(item))
        {
            RemoveStackableItemFromInventory(inventoryItems, item);
        }
        else
        {
            RemoveNonStackableItemFromInventory(inventoryItems, item);
        }
    }

    private void RemoveStackableItemFromInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        if (inventoryItems.StackableItems.StackableItemSpaces.Any(space => space.FriendlyID == item.FriendlyID && space.Amount > 0))
        {
            inventoryItems.StackableItems.StackableItemSpaces.FirstOrDefault(space => space.FriendlyID == item.FriendlyID).Amount--;
        }
    }

    private void RemoveNonStackableItemFromInventory(InventoryItems inventoryItems, ItemInstance item)
    {
        switch (item)
        {
            case ArmorInstance armorInstance:
                inventoryItems.NonStackableItems.NonStackableArmorInstances.Remove(
                    inventoryItems.NonStackableItems.NonStackableArmorInstances.FirstOrDefault(
                        item => item.InstanceID == armorInstance.InstanceID));
                break;
            case WeaponInstance weaponInstance:
                inventoryItems.NonStackableItems.NonStackableWeaponInstances.Remove(
                    inventoryItems.NonStackableItems.NonStackableWeaponInstances.FirstOrDefault(
                        item => item.InstanceID == weaponInstance.InstanceID));
                break;
            case BuffInstance buffInstance:
                inventoryItems.NonStackableItems.NonStackableBuffInstances.Remove(
                    inventoryItems.NonStackableItems.NonStackableBuffInstances.FirstOrDefault(
                        item => item.InstanceID == buffInstance.InstanceID));
                break;
        }
    }

    private bool IsItemStackable(ItemInstance item)
    {
        return item is ConsumableInstance;
    }
}
