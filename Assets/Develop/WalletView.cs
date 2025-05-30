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
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        _wallet.ValueChanged += OnCurrencyChanged;

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value);
=======
        _currencyController = currencyController;        

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value.Value);
            _wallet.SubscribeToCurrencyChange(currency.Key, (newValue) => OnCurrencyChanged(currency.Key, newValue));
>>>>>>> Stashed changes
=======
        _currencyController = currencyController;        

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value.Value);
            _wallet.SubscribeToCurrencyChange(currency.Key, (newValue) => OnCurrencyChanged(currency.Key, newValue));
>>>>>>> Stashed changes
=======
        _currencyController = currencyController;        

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value.Value);
            _wallet.SubscribeToCurrencyChange(currency.Key, (newValue) => OnCurrencyChanged(currency.Key, newValue));
>>>>>>> Stashed changes
=======
        _currencyController = currencyController;        

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value.Value);
            _wallet.SubscribeToCurrencyChange(currency.Key, (newValue) => OnCurrencyChanged(currency.Key, newValue));
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
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
=======
    private void OnCurrencyChanged(CurrencyType changedCurrencyType, int newValue)
>>>>>>> Stashed changes
    {
        if (_currencyViews.TryGetValue(changedCurrencyType, out var currencyView))
        {
=======
    private void OnCurrencyChanged(CurrencyType changedCurrencyType, int newValue)
    {
        if (_currencyViews.TryGetValue(changedCurrencyType, out var currencyView))
        {
>>>>>>> Stashed changes
=======
    private void OnCurrencyChanged(CurrencyType changedCurrencyType, int newValue)
    {
        if (_currencyViews.TryGetValue(changedCurrencyType, out var currencyView))
        {
>>>>>>> Stashed changes
=======
    private void OnCurrencyChanged(CurrencyType changedCurrencyType, int newValue)
    {
        if (_currencyViews.TryGetValue(changedCurrencyType, out var currencyView))
        {
>>>>>>> Stashed changes
            currencyView.UpdateAmount(newValue);
        }
        else
        {
            CreateCurrencyView(changedCurrencyType, newValue);
        }
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            foreach (var currency in _wallet.Storage)
            {
                if (currency.Value != null)
                {
                    _wallet.UnsubscribeFromCurrencyChange(currency.Key, (newValue) => OnCurrencyChanged(currency.Key, newValue));
                }
            }
        }
    }
}

