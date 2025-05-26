public interface IWallet : IReadOnlyWallet
{
    void AddCurrency(CurrencyType type, int amount);
    void RemoveCurrency(CurrencyType type, int amount);
}
