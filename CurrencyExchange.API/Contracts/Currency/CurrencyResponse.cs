namespace CurrencyExchange.API.Contracts.Currency
{
    public record CurrencyResponse(int CurrencyId, string Code, string FullName, string Sign);
}
