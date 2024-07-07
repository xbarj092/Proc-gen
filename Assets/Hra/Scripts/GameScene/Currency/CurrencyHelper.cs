using System.Collections.Generic;

public class CurrencyHelper
{
    public void ChangeCurrency(float amount, bool add)
    {
        if (add)
        {
            AddCurrency(amount);
        }
        else
        {
            SpendCurrency(amount);
        }
    }

    private void AddCurrency(float amount)
    {
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        currencyData.CurrencyAmount += amount;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
    }

    private void SpendCurrency(float amount)
    {
        CurrencyData currencyData = LocalDataStorage.Instance.PlayerData.CurrencyData;
        currencyData.CurrencyAmount -= amount;
        LocalDataStorage.Instance.PlayerData.CurrencyData = currencyData;
    }

    public void AddCurrencyFromItems(List<ItemInstance> items)
    {
        float totalAmountObtained = default;
        foreach (ItemInstance item in items)
        {
            float itemPrice = item.Price / 10;
            totalAmountObtained += itemPrice;
        }

        AddCurrency(totalAmountObtained);
    }
}
