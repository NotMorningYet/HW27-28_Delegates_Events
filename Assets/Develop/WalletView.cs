using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Transform _currencyContainer;
    [SerializeField] private CurrencyView _currencyPrefab;

    private CurrencyController _currencyController;

    private Wallet _wallet;
    private Dictionary<CurrencyType, CurrencyView> _currencyViews = new Dictionary<CurrencyType, CurrencyView>();

    public void Initialize(Wallet wallet, CurrencyController currencyController)
    {
        _wallet = wallet;
        _currencyController = currencyController;

        _wallet.ValueChanged += OnCurrencyChanged;

        foreach (var currency in wallet.Storage)
        {
            CreateCurrencyView(currency.Key, currency.Value);
            OnCurrencyChanged(currency.Key);
        }
    }

    private void CreateCurrencyView(CurrencyType type, int amount)
    {
        if (_currencyViews.ContainsKey(type))
            return;

        CurrencyView currency = Instantiate(_currencyPrefab, _currencyContainer);
        var currencyView = currency.GetComponent<CurrencyView>();
        currencyView.Initialize(type.ToString(), amount, _currencyController.GetCurrencyByType(type).Icon);

        _currencyViews.Add(type, currencyView);
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

