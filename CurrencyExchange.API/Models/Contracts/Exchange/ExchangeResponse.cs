using CurrencyExchange.API.Models.Contracts.Currency;

namespace CurrencyExchange.API.Models.Contracts.Exchange;

public record ExchangeResponse(
    CurrencyResponse BaseCurrency,
    CurrencyResponse TargetCurrency,
    decimal Rate,
    decimal Amount,
    decimal ConvertedAmount
    );