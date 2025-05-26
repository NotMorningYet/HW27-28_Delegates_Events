using System;
using System.Collections.Generic;

public interface IReadOnlyWallet
{
    IReadOnlyDictionary<CurrencyType, int> Storage { get; }
    event Action<CurrencyType> ValueChanged;
}
