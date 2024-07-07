using System;
using System.Collections.Generic;
using System.Linq;

public class ShopHelper
{
    private CurrencyHelper _currencyHelper = new();
    private InventoryHelper _inventoryHelper = new();
    private ItemGenerator _itemGenerator = new();
    private DiscountHelper _discountHelper = new();

    public void PurchaseItem(ItemInstance itemPurchased)
    {
        _currencyHelper.ChangeCurrency(itemPurchased.Price, false);
        _inventoryHelper.AddItemsToInventory(new() { itemPurchased });
    }

    public void SellItems(List<ItemInstance> items)
    {
        _inventoryHelper.RemoveItems(items);
        _currencyHelper.AddCurrencyFromItems(items);
    }

    public List<ItemInstance> GetNewItems()
    {
        List<ItemInstance> newItems = _itemGenerator.GenerateItems(3);
        List<ItemInstance> discountedItems = _discountHelper.GetDiscountedItems(newItems);
        return _discountHelper.UpdateWithDiscountedItems(newItems, discountedItems);
    }
}
