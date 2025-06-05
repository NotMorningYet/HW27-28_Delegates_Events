using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private WalletActionsHandler _walletActionsHandler;

    private Wallet _wallet;
    
    private void Awake()
    {
        _wallet = new Wallet();
        InitializeDefaultCurrencies();
        _walletView.Initialize(_wallet);
        _walletActionsHandler.Initialize(_wallet);
        _walletActionsHandler.gameObject.SetActive(true);
    }

    private void InitializeDefaultCurrencies()
    {
        _wallet.AddCurrency(CurrencyType.Gem, 0);
        _wallet.AddCurrency(CurrencyType.Coin, 0);
        _wallet.AddCurrency(CurrencyType.Food, 0);
    }
}
