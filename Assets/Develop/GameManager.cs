using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private WalletView _walletView;
    [SerializeField] private WalletActionsHandler _walletActionsHandler;

    private Wallet _wallet;
    
    private void Awake()
    {
        _wallet = new Wallet(CreateCurrencyStorage());
        _walletView.Initialize(_wallet);
        _walletActionsHandler.Initialize(_wallet);
        _walletActionsHandler.gameObject.SetActive(true);
    }

    private Dictionary<CurrencyType, ReactiveVariable<int>> CreateCurrencyStorage()
    {
        Dictionary<CurrencyType, ReactiveVariable<int>> storage = new Dictionary<CurrencyType, ReactiveVariable<int>>
        {
            { CurrencyType.Gem, new ReactiveVariable<int>(0) },
            { CurrencyType.Coin, new ReactiveVariable<int>(0) },
            { CurrencyType.Food, new ReactiveVariable <int>(0) }
        };

        return storage;
    }
}
