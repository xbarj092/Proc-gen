using System;
using Newtonsoft.Json;

[Serializable]
public class ShopDiscountData
{
    [JsonProperty("ShopDiscountThresholds")]
    public ShopDiscountThreshold[] ShopDiscountThresholds;

    public ShopDiscountData(ShopDiscountThreshold[] shopDiscountThresholds)
    {
        ShopDiscountThresholds = shopDiscountThresholds;
    }
}
