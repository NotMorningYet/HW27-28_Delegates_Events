using UnityEngine;

[CreateAssetMenu(fileName = "CurrencyViewConfig", menuName = "Game/Currency View Config")]

public class CurrencyViewConfig : ScriptableObject
{
    public string Title;
    public CurrencyType Type;
    public Sprite Icon;
}