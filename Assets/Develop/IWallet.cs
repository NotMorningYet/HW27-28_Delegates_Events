public interface IWallet : IReadOnlyWallet
{
    void AddNewCurrency(CurrencyType type, int amount);
    void AddCurrency(CurrencyType type, int amount);
    void RemoveCurrency(CurrencyType type, int amount);
}
