using System;
using System.Collections.Generic;
using System.Linq;

public class ShopHelper
{
    private CurrencyHelper _currencyHelper = new();
    private InventoryHelper _inventoryHelper = new();
    private ItemGenerator _itemGenerator = new();

    private const float SHOP_DISCOUNT_CHANCE = 0.25f;
    private static readonly Random random = new();

    public void PurchaseItem(ItemInstance itemPurchased)
    {
        _currencyHelper.ChangeCurrency(itemPurchased.Price, false);
        _inventoryHelper.AddItemsToInventory(new() { itemPurchased });
    }

    public List<ItemInstance> GetNewItems()
    {
        List<ItemInstance> newItems = _itemGenerator.GenerateItems(3);
        List<ItemInstance> discountedItems = GetDiscountedItems(newItems);
        return UpdateWithDiscountedItems(newItems, discountedItems);
    }

    private List<ItemInstance> GetDiscountedItems(List<ItemInstance> newItems)
    {
        List<ItemInstance> discountedItems = new();
        foreach (ItemInstance item in newItems)
        {
            Random random = new();
            double randomDouble = random.NextDouble();
            if (randomDouble <= SHOP_DISCOUNT_CHANCE)
            {
                float discount = GetDiscountPercentage();
                item.Price -= discount * item.Price;
                discountedItems.Add(item);
            }
        }

        return discountedItems;
    }

    private float GetDiscountPercentage()
    {
        double randomValue = random.NextDouble();
        double cumulativeProbability = default;
        ShopDiscountData discountData = LocalDataStorage.Instance.TitleData.ShopDiscountData;

        foreach (ShopDiscountThreshold discountThreshold in discountData.ShopDiscountThresholds)
        {
            cumulativeProbability += discountThreshold.DiscountChance;
            if (randomValue <= cumulativeProbability)
            {
                return discountThreshold.DiscountPercentage;
            }
        }

        return default;
    }

    private List<ItemInstance> UpdateWithDiscountedItems(List<ItemInstance> originalItems, List<ItemInstance> discountedItems)
    {
        Dictionary<string, ItemInstance> discountedItemsDict = discountedItems.ToDictionary(item => item.FriendlyID);
        for (int i = 0; i < originalItems.Count; i++)
        {
            if (discountedItemsDict.TryGetValue(originalItems[i].FriendlyID, out ItemInstance discountedItem))
            {
                originalItems[i] = discountedItem;
            }
        }

        return originalItems;
    }
}
