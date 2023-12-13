using CurrencyExchange.API.Models.Contracts.Currency;

namespace CurrencyExchange.API.Models.Contracts.ExchangeRate
{
    public record ExchangeRateResponse(
        int ExchangeRateId,
        CurrencyResponse BaseCurrency,
        CurrencyResponse TargetCurrency,
        decimal Rate);
}
