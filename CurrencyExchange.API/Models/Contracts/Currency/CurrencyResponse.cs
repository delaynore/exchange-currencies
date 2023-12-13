namespace CurrencyExchange.API.Models.Contracts.Currency
{
    public record CurrencyResponse(int CurrencyId, string Code, string FullName, string Sign);
}
