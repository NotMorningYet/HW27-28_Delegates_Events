using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletActionsHandler : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _currencyTypeDropdown;
    [SerializeField] private TMP_InputField _amountInput;
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _removeButton;
    
    private Wallet _wallet;

    public void Initialize(Wallet wallet)
    {
        _wallet = wallet;

        _addButton.onClick.AddListener(OnAddCurrencyClick);
        _removeButton.onClick.AddListener(OnRemoveCurrencyClick);

        InitializeCurrencyDropdown();
    }

    private void OnAddCurrencyClick()
    {
        if (TryGetSelectedCurrency(out var type) && TryGetInputAmount(out var amount))
            _wallet.AddCurrency(type, amount);
    }

    private void OnRemoveCurrencyClick()
    {
        if (TryGetSelectedCurrency(out var type) && TryGetInputAmount(out var amount))
            _wallet.RemoveCurrency(type, amount);
    }

    private void InitializeCurrencyDropdown()
    {
        _currencyTypeDropdown.ClearOptions();

        var currencyTypes = Enum.GetValues(typeof(CurrencyType));
        var options = new List<TMP_Dropdown.OptionData>();

        foreach (CurrencyType type in currencyTypes)
            options.Add(new TMP_Dropdown.OptionData(type.ToString()));

        _currencyTypeDropdown.AddOptions(options);
    }

    private bool TryGetSelectedCurrency(out CurrencyType type)
    {
        var selected = _currencyTypeDropdown.options[_currencyTypeDropdown.value].text;
        return Enum.TryParse(selected, out type);
    }

    private bool TryGetInputAmount(out int amount) => int.TryParse(_amountInput.text, out amount);
    
    private void OnDestroy()
    {
        _addButton.onClick.RemoveAllListeners();
        _removeButton.onClick.RemoveAllListeners();
    }

}
