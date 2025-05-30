using System.Collections.Generic;

public interface IReadOnlyWallet
{
    IReadOnlyDictionary<CurrencyType, ReactiveVariable<int>> Storage { get; }
}
