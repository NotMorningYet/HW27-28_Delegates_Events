using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WalletUI _walletUI;
    [SerializeField] private WalletActionsHandler _walletActionsHandler;

    private CurrencyController _currencyController;
    private Wallet _wallet;
    
    private void Awake()
    {
        _wallet = new Wallet();
        _currencyController = new CurrencyController();
        _walletUI.Initialize(_wallet, _currencyController);
        _walletActionsHandler.Initialize(_wallet);
        _walletActionsHandler.gameObject.SetActive(true);
    }
}
