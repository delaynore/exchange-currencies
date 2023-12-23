using CurrencyExchange.API.Contracts.Currency;

namespace CurrencyExchange.API.Contracts.Exchange;

public record ExchangeResponse(
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate,
    decimal Amount,
    decimal ConvertedAmount
    );