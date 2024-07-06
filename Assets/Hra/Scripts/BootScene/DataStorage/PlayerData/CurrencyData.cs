using System;

[Serializable]
public class CurrencyData
{
    public float CurrencyAmount;

    public CurrencyData(float currencyAmount)
    {
        CurrencyAmount = currencyAmount;
    }
}
