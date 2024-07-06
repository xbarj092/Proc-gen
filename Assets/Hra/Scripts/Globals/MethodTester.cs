using EasyButtons;
using System.Collections.Generic;
using UnityEngine;

public class MethodTester : MonoBehaviour
{
    private SpecialEffectHandler _specialEffectHandler = new();
    private InventoryHelper _inventoryHelper = new();
    private CurrencyHelper _currencyHelper = new();
    private ShopHelper _shopHelper = new();

    private ItemGenerator _itemGenerator = new();

    [SerializeField] private ItemBase _item;

    [Button]
    public void GrantBuffSpecialEffects()
    {
        switch (_item)
        {
            case BuffItem buffItem:
                _specialEffectHandler.ApplyBuffEffects(buffItem, true);
                break;
            case ArmorItem armorItem:
                _specialEffectHandler.ApplyArmorEffects(armorItem, true);
                break;
            case ConsumableItem consumableItem:
                _specialEffectHandler.ApplyConsumableEffects(consumableItem, true);
                break;
        }
    }

    [Button]
    public void GenerateRandomItem()
    {
        _inventoryHelper.AddItemsToInventory(_itemGenerator.GenerateItems(5));
    }

    [Button]
    public void AddCurrency()
    {
        _currencyHelper.ChangeCurrency(500, true);
    }

    [Button]
    public void GetShopItems()
    {
        List<ItemInstance> newItems = _shopHelper.GetNewItems();
        foreach (ItemInstance item in newItems)
        {
            Debug.Log(item.FriendlyID + ": " + item.Price);
        }
    }
}
