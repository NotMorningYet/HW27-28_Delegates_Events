using UnityEngine;

public class Currency 
{
    public Currency(string title, CurrencyType type, Sprite icon)
    {
        Title = title;
        Type = type;
        Icon = icon;
    }

    public string Title { get; }
    public CurrencyType Type { get; }
    public Sprite Icon { get; }
       
}
