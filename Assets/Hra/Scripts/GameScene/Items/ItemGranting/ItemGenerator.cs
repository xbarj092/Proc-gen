using System;
using System.Collections.Generic;
using System.Linq;

public class ItemGenerator
{
    public const float DEFAULT_WEIGHT_VALUE = 0.8f;

    public List<ItemBase> GrantItems(int amount)
    {
        List<ItemBase> allItems = LocalDataStorage.Instance.CatalogItems.CatalogItemList;
        Dictionary<ItemBase, float> itemRarities = GetCatalogItemRarities(allItems);
        return ChooseItemsBasedOnRarity(itemRarities, amount);
    }

    private Dictionary<ItemBase, float> GetCatalogItemRarities(List<ItemBase> allItems)
    {
        Dictionary<ItemBase, float> catalogItemRarities = new();
        foreach (ItemBase catalogItem in allItems)
        {
            float itemWeight = GetRarityWeight(catalogItem);
            catalogItemRarities.Add(catalogItem, itemWeight);
        }

        return catalogItemRarities;
    }

    private float GetRarityWeight(ItemBase catalogItem)
    {
        string rarityType = catalogItem.Rarity.ToString();
        if (rarityType == null)
        {
            return DEFAULT_WEIGHT_VALUE;
        }

        foreach (RarityData rarityData in LocalDataStorage.Instance.TitleData.ItemRarities.RarityData)
        {
            if (rarityType == rarityData.Type)
            {
                return rarityData.Rarity;
            }
        }

        return DEFAULT_WEIGHT_VALUE;
    }

    private List<ItemBase> ChooseItemsBasedOnRarity(Dictionary<ItemBase, float> catalogItemRarities, int amountOfItems)
    {
        List<ItemBase> selectedItems = new();
        Random random = new();
        double totalWeight = catalogItemRarities.Values.Sum();
        for (int i = 0; i < amountOfItems; i++)
        {
            double randomNumber = random.NextDouble() * totalWeight;
            selectedItems.Add(SelectItemBasedOnRandomNumber(catalogItemRarities, randomNumber));
        }

        return selectedItems;
    }

    private ItemBase SelectItemBasedOnRandomNumber(Dictionary<ItemBase, float> catalogItemRarities, double randomNumber)
    {
        foreach (ItemBase catalogItem in catalogItemRarities.Keys)
        {
            if (randomNumber < catalogItemRarities[catalogItem])
            {
                return catalogItem;
            }

            randomNumber -= catalogItemRarities[catalogItem];
        }

        return null;
    }
}
