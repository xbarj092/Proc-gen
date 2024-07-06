using System;
using Newtonsoft.Json;

[Serializable]
public class ShopDiscountThreshold
{
    [JsonProperty("DiscountPercentage")]
    public float DiscountPercentage;
    [JsonProperty("DiscountChance")]
    public float DiscountChance;

    public ShopDiscountThreshold(float discountPercentage, float discountChance)
    {
        DiscountPercentage = discountPercentage;
        DiscountChance = discountChance;
    }
}
