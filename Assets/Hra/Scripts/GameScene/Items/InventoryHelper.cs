using System.Collections.Generic;
using System.Linq;

public class InventoryHelper
{
    public void AddItemsToInventory(List<ItemBase> chosenItems)
    {
        InventoryItems inventoryItems = LocalDataStorage.Instance.PlayerData.InventoryItems;
        foreach (ItemBase item in chosenItems)
        {
            AddItemToInventory(inventoryItems, item);
        }

        LocalDataStorage.Instance.PlayerData.InventoryItems = inventoryItems;
    }

    private void AddItemToInventory(InventoryItems inventoryItems, ItemBase item)
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

    private void AddStackableItemToInventory(InventoryItems inventoryItems, ItemBase item)
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

    private void AddNonStackableItemToInventory(InventoryItems inventoryItems, ItemBase item)
    {
        switch (item)
        {
            case ArmorItem armorItem:
                inventoryItems.NonStackableItems.NonStackableArmorInstances.Add(
                    new(armorItem.FriendlyID, armorItem.Rarity, armorItem.SpecialArmorEffects, armorItem.Defense));
                break;
            case WeaponItem weaponItem:
                inventoryItems.NonStackableItems.NonStackableWeaponInstances.Add(
                    new(weaponItem.FriendlyID, weaponItem.Rarity, weaponItem.Damage, weaponItem.Range, weaponItem.AttacksPerSecond, weaponItem.Pierce));
                break;
            case BuffItem buffItem:
                inventoryItems.NonStackableItems.NonStackableBuffInstances.Add(
                    new(buffItem.FriendlyID, buffItem.Rarity, buffItem.SpecialBuffEffects));
                break;
        }
    }

    private bool IsItemStackable(ItemBase item)
    {
        return item is ConsumableItem;
    }
}
