using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWallet
{
    public event Action<CurrencyType> ValueChanged;

    private readonly Dictionary<CurrencyType, int> _storage;

    public Wallet(Dictionary<CurrencyType, int> storage)
    {        
        _storage = storage;
    }

    public IReadOnlyDictionary<CurrencyType, int> Storage => _storage;

    public void AddCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
        {
            ShowMessageInvalidAmount();
            return;
        }

        if (_storage.ContainsKey(type))
            _storage[type] += amount;
        else
            _storage.Add(type, amount);

        ValueChanged?.Invoke(type);
    }

    public void RemoveCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
        {
            ShowMessageInvalidAmount();
            return;
        }

        if (_storage.ContainsKey(type))
        {
            if (IsEnough(type, amount))
            {
                _storage[type] -= amount;
            }
            else
            {
                ShowMessageNotEnough(type);
            }

            ValueChanged?.Invoke(type);
        }
        else
        {
            ShowMessageCurrencyTypeDoesNotExist(type);
        }
    }

    private bool IsEnough(CurrencyType type, int amount) => _storage[type] >= amount;
    
    private void ShowMessageNotEnough(CurrencyType type)
    {
        Debug.Log($"Недостаточно средств {type}");
    }

    private void ShowMessageCurrencyTypeDoesNotExist(CurrencyType type)
    {
        Debug.Log($"В кошельке отсутствует валюта {type}");
    }

    private void ShowMessageInvalidAmount()
    {
        Debug.Log("Значение валюты не может быть отрицательным");
    }
}
