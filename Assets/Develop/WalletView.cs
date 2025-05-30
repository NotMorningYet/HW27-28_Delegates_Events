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
        _wallet.ValueChanged += OnCurrencyChanged;

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value);
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

    private void OnCurrencyChanged(CurrencyType changedCurrencyType)
    {

        if (_wallet.Storage.TryGetValue(changedCurrencyType, out var newAmount))
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
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            _wallet.ValueChanged -= OnCurrencyChanged;
        }
    }
}

