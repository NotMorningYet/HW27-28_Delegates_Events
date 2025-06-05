using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _amountText;

    private CurrencyType _type;
    private IReadOnlyReactiveVariable<int> _currencyAmount;

    public void Initialize(CurrencyViewConfig config, IReadOnlyReactiveVariable<int> currencyAmount)
    {
        _type = config.Type;
        _titleText.text = config.Title;
        _icon.sprite = config.Icon;

        SetupCurrency(currencyAmount);
    }

    public void SetupCurrency(IReadOnlyReactiveVariable<int> currencyAmount)
    {
        if (_currencyAmount != null)
            _currencyAmount.Changed -= OnAmountChanged;

        _currencyAmount = currencyAmount;
        UpdateAmount(_currencyAmount.Value);

        _currencyAmount.Changed += OnAmountChanged;
    }

    private void OnAmountChanged(int newAmount)
    {
        UpdateAmount(newAmount);
    }

    private void UpdateAmount(int amount)
    {
        _amountText.text = amount.ToString();
    }

    private void OnDestroy()
    {
        if (_currencyAmount != null)
            _currencyAmount.Changed -= OnAmountChanged;
    }
}

