using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWallet
{
    public event Action ValueChanged;
    public event Action<Dictionary<CurrencyType, int>> WalletCreated;

    private readonly Dictionary<CurrencyType, int> _storage = new();

    public Wallet()
    {        
        WalletCreated?.Invoke(_storage);
    }

    public IReadOnlyDictionary<CurrencyType, int> Storage => _storage;

    public void AddNewCurrency(CurrencyType type, int amount)
    {
        _storage.Add(type, amount);
    }

    public void AddCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
        {
            InvalidAmount();
            return;
        }

        if (_storage.ContainsKey(type))
            _storage[type] += amount;
        else
            AddNewCurrency(type, amount);

        ValueChanged?.Invoke();
    }

    public void RemoveCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
        {
            InvalidAmount();
            return;
        }

        if (_storage.ContainsKey(type))
        {
            if (IsEnough(type, amount))
            {
                _storage[type] -= amount;
            }
            else
                NotEnough(type);

            ValueChanged?.Invoke();
        }
        else 
            CurrencyTypeDoesNotExist(type);
    }

    private bool IsEnough(CurrencyType type, int amount) => _storage[type] >= amount;
    
    private void NotEnough(CurrencyType type)
    {
        Debug.Log($"Недостаточно средств {type}");
    }

    private void CurrencyTypeDoesNotExist(CurrencyType type)
    {
        Debug.Log($"В кошельке отсутствует валюта {type}");
    }

    private void InvalidAmount()
    {
        Debug.Log("Значение валюты не может быть отрицательным");
    }
}
