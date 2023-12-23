using CurrencyExchange.API.Contracts.Currency;

namespace CurrencyExchange.API.Contracts.ExchangeRate
{
    public record ExchangeRateResponse(
        int ExchangeRateId,
        CurrencyResponse BaseCurrency,
        CurrencyResponse TargetCurrency,
        decimal Rate);
}
