using System;
using System.Collections.Generic;
using UnityEngine;

public class Wallet : IWallet
{
    private readonly Dictionary<CurrencyType, ReactiveVariable<int>> _storage;

    public Wallet(Dictionary<CurrencyType, ReactiveVariable<int>> storage)
    {        
        _storage = storage;
    }

    public IReadOnlyDictionary<CurrencyType, ReactiveVariable<int>> Storage => _storage;

    public void AddCurrency(CurrencyType type, int amount)
    {
        if (amount < 0)
        {
            ShowMessageInvalidAmount();
            return;
        }

        if (_storage.ContainsKey(type))
            _storage[type].Value += amount;
        else
            _storage.Add(type, new ReactiveVariable<int>(amount));                
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
                _storage[type].Value -= amount;
            }
            else
            {
                ShowMessageNotEnough(type);
            }
        }
        else
        {
            ShowMessageCurrencyTypeDoesNotExist(type);
        }
    }

    public void SubscribeToCurrencyChange(CurrencyType type, Action<int> callback)
    {
        if (_storage.TryGetValue(type, out var reactiveVariable))
        {
            reactiveVariable.Changed += callback;
        }
    }

    public void UnsubscribeFromCurrencyChange(CurrencyType type, Action<int> callback)
    {
        if (_storage.TryGetValue(type, out var reactiveVariable))
            reactiveVariable.Changed -= callback;
    }

    private bool IsEnough(CurrencyType type, int amount) => _storage[type].Value >= amount;
    
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
