using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Transform _currencyContainer;
    [SerializeField] private CurrencyView _currencyPrefab;
    [SerializeField] private CurrencyViewConfig[] _currencyConfigs;

    private Dictionary<CurrencyType, CurrencyView> _currencyViews = new();

    public void Initialize(Wallet wallet)
    {
        ClearExistingViews();

        foreach (var currency in wallet.Storage)
            CreateCurrencyView(currency.Key, currency.Value);
    }

    private void CreateCurrencyView(CurrencyType type, IReadOnlyReactiveVariable<int> currencyAmount)
    {
        if (_currencyViews.ContainsKey(type)) 
            return;

        CurrencyViewConfig config = GetConfigByType(type);

        if (config == null) 
            return;

        CurrencyView view = Instantiate(_currencyPrefab, _currencyContainer);
        view.Initialize(config, currencyAmount);
        _currencyViews.Add(type, view);
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

    private void ClearExistingViews()
    {
        foreach (CurrencyView view in _currencyViews.Values)
            Destroy(view.gameObject);

        _currencyViews.Clear();
    }

    private void OnDestroy()
    {
        ClearExistingViews();
    }
}

