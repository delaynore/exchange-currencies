using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Models.Contracts.Exchange;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services;

public class ExchangeService(IExchangeRateRepository exchangeRateRepository, ICurrencyRepository currencyRepository) : IExchangeService
{
    public Result<ExchangeResponse> Exchange(string baseCode, string targetCode, decimal amount)
    {
        if (baseCode is null)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(baseCode)));
        if (targetCode is null)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(targetCode)));
        if (baseCode.Length != 3)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("base"));
        if (targetCode.Length != 3)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("target"));
        if (amount <= 0)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeErrors.NegativeOrZeroAmount());

        var potentialRate = exchangeRateRepository.FindSimilarRate(baseCode, targetCode);
        if (!potentialRate.HasValue)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeErrors.NotFound());

        var baseCurrency = currencyRepository.GetCurrencyByCode(baseCode)!;
        var targetCurrency = currencyRepository.GetCurrencyByCode(targetCode)!;
        
            return new ExchangeResponse(
                new CurrencyResponse(
                    baseCurrency.Id,
                    baseCurrency.Code,
                    baseCurrency.FullName,
                    baseCurrency.Sign),
                new CurrencyResponse(
                    targetCurrency.Id,
                    targetCurrency.Code,
                    targetCurrency.FullName,
                    targetCurrency.Sign),
                decimal.Round(potentialRate.Value, 4),
                amount,
                decimal.Round(potentialRate.Value * amount)
                    );
    }
}