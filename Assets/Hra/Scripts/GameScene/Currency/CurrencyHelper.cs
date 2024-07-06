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
}
