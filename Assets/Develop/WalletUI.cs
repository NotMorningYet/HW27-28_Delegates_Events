using System.Collections.Generic;
using UnityEngine;


public class WalletUI : MonoBehaviour
{
    [SerializeField] private Transform _currencyContainer;
    [SerializeField] private CurrencyUI _currencyPrefab;

    private CurrencyController _currencyController;

    private Wallet _wallet;
    private Dictionary<CurrencyType, CurrencyUI> _currencyUIs = new Dictionary<CurrencyType, CurrencyUI>();

    public void Initialize(Wallet wallet, CurrencyController currencyController)
    {
        _wallet = wallet;
        _currencyController = currencyController;

        _wallet.WalletCreated += OnWalletCreated;
        _wallet.ValueChanged += UpdateUI;
    }

    private void OnWalletCreated(Dictionary<CurrencyType, int> storage)
    {
        foreach (var currency in storage)
            CreateCurrencyUI(currency.Key, currency.Value);

        UpdateUI();
    }

    private void CreateCurrencyUI(CurrencyType type, int amount)
    {
        if (_currencyUIs.ContainsKey(type)) 
            return;

        CurrencyUI currency = Instantiate(_currencyPrefab, _currencyContainer);
        var currencyUI = currency.GetComponent<CurrencyUI>();        
        currencyUI.Initialize(type.ToString(), amount, _currencyController.GetCurrencyByType(type).Icon);

        _currencyUIs.Add(type, currencyUI);
    }

    private void UpdateUI()
    {
        foreach (var currency in _wallet.Storage)
        {
            if (_currencyUIs.TryGetValue(currency.Key, out var currencyUI))
            {
                currencyUI.UpdateAmount(currency.Value);
            }
            else
            {
                CreateCurrencyUI(currency.Key, currency.Value);
            }
        }
    }

    private void OnDestroy()
    {
        if (_wallet != null)
        {
            _wallet.WalletCreated -= OnWalletCreated;
            _wallet.ValueChanged -= UpdateUI;
        }
    }
}

