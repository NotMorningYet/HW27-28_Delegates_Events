using System.Collections.Generic;

public interface IReadOnlyWallet
{
    IReadOnlyDictionary<CurrencyType, IReadOnlyReactiveVariable<int>> Storage { get; }
}
