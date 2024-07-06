using System;
using UnityEngine;

[Serializable]
public class TitleData
{
    private TitleDataLoader _titleDataLoader = new();

    [field: SerializeField] public ItemRarityTitleData ItemRarities { get; private set; }
    [field: SerializeField] public ShopDiscountData ShopDiscountData { get; private set; }

    private const string TITLE_DATA_PATH_RARITY = "TitleData/ItemRarities";
    private const string TITLE_DATA_PATH_DISCOUNT = "TitleData/ShopDiscountData";

    public void SetUpTitleData()
    {
        ItemRarities = _titleDataLoader.LoadTitleData<ItemRarityTitleData>(TITLE_DATA_PATH_RARITY);
        ShopDiscountData = _titleDataLoader.LoadTitleData<ShopDiscountData>(TITLE_DATA_PATH_DISCOUNT);
    }
}
