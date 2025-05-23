using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyUI : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private TMP_Text _amountText;

    public void Initialize(string title, int amount, Sprite icon)
    {
        _titleText.text = title;
        _amountText.text = amount.ToString();
        _icon.sprite = icon;
    }

    public void UpdateAmount(int newAmount)
    {
        _amountText.text = newAmount.ToString();
    }
}

