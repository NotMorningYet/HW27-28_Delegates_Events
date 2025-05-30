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

    private Dictionary<CurrencyType, int> CreateCurrencyStorage()
    {
        Dictionary<CurrencyType, int> storage = new Dictionary<CurrencyType, int>
        {
            { CurrencyType.Gem, 0 },
            { CurrencyType.Coin, 0 },
            { CurrencyType.Food, 0 }
        };

        return storage;
    }
}
