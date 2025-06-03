using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Transform _currencyContainer;
    [SerializeField] private CurrencyView _currencyPrefab;
    [SerializeField] private CurrencyViewConfig[] _currencyConfigs;

    private Wallet _wallet;
    private Dictionary<CurrencyType, CurrencyView> _currencyViews = new Dictionary<CurrencyType, CurrencyView>();

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;

        foreach (var currency in wallet.Storage)
        {
            SubscribeToWallet(currency.Key, currency.Value);
            CreateCurrencyView(currency.Key, currency.Value.Value);
        }
    }

    private void SubscribeToWallet(CurrencyType type, ReactiveVariable<int> value)
    {
        value.Changed += (newValue) => OnCurrencyChanged(type, newValue);
    }

    private void UnsubscribeFromWallet()
    {
        foreach (var currency in _wallet.Storage)
        {
            var reactiveVar = currency.Value;
            reactiveVar.Changed -= (newValue) => OnCurrencyChanged(currency.Key, newValue);
        }
    }

    private void CreateCurrencyView(CurrencyType type, int amount)
    {
        if (_currencyViews.ContainsKey(type))
            return;

        var config = GetConfigByType(type);
        if (config == null) return;

        CurrencyView currencyView = Instantiate(_currencyPrefab, _currencyContainer);
        currencyView.Initialize(config.Title, amount, config.Icon);
        _currencyViews.Add(type, currencyView);
    }

    private CurrencyViewConfig GetConfigByType(CurrencyType type)
    {
        foreach (var config in _currencyConfigs)
        {
            if (config.Type == type)
                return config;
        }
        return null;
    }

    private void OnCurrencyChanged(CurrencyType changedCurrencyType, int newAmount)
    {
        if (_currencyViews.TryGetValue(changedCurrencyType, out var currencyView))
        {
            currencyView.UpdateAmount(newAmount);
        }
        else
        {
            CreateCurrencyView(changedCurrencyType, newAmount);
        }
    }

    private void OnDestroy()
    {
        UnsubscribeFromWallet();
    }
}

