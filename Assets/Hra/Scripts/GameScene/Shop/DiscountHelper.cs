using System;
using System.Collections.Generic;
using System.Linq;

public class DiscountHelper
{
    private const float SHOP_DISCOUNT_CHANCE = 0.25f;
    private static readonly Random random = new();

    public List<ItemInstance> GetDiscountedItems(List<ItemInstance> newItems)
    {
        List<ItemInstance> discountedItems = new();
        foreach (ItemInstance item in newItems)
        {
            Random random = new();
            double randomDouble = random.NextDouble();
            if (randomDouble <= SHOP_DISCOUNT_CHANCE)
            {
                float discount = GetDiscountPercentage();
                item.DiscountedPrice = UnityEngine.Mathf.CeilToInt(discount * item.Price);
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

    public List<ItemInstance> UpdateWithDiscountedItems(List<ItemInstance> originalItems, List<ItemInstance> discountedItems)
    {
        Dictionary<string, ItemInstance> discountedItemsDict = discountedItems.GroupBy(item => item.FriendlyID)
            .ToDictionary(group => group.Key, group => group.First());

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
