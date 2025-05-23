using System.Collections.Generic;
using UnityEngine;

public class CurrencyController
{
    private List<Currency> _currencies = new List<Currency>();
    
    public CurrencyController()
    {
        CreateCurrencies();
    }

    public List<Currency> Currencies => _currencies;

    private void CreateCurrencies()
    {
        var coinIcon = Resources.Load<Sprite>("Art/Icons/coin");
        Debug.Log($"Coin icon loaded: {coinIcon != null}");

        _currencies.Add(new Currency("Coin", CurrencyType.Coin, Resources.Load<Sprite>("Art/Icons/coin")));
        _currencies.Add(new Currency("Gem", CurrencyType.Gem, Resources.Load<Sprite>("Art/Icons/gem")));
        _currencies.Add(new Currency("Food", CurrencyType.Food, Resources.Load<Sprite>("Art/Icons/food")));
    }

    public Currency GetCurrencyByType(CurrencyType targetType) => _currencies.Find(currency => currency.Type == targetType);
}
