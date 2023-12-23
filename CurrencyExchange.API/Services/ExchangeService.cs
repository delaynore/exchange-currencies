using AutoMapper;
using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Contracts.Exchange;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services;

public class ExchangeService(
    IExchangeRateRepository exchangeRateRepository,
    ICurrencyRepository currencyRepository,
    IMapper mapper) : IExchangeService
{
    public async Task<Result<ExchangeResponse>> Exchange(string baseCode, string targetCode, decimal amount)
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

        var potentialRate =  await exchangeRateRepository.FindSimilarRate(baseCode, targetCode);
        if (!potentialRate.HasValue)
            return Result.Failure<ExchangeResponse>(ApplicationErrors.ExchangeErrors.NotFound());

        var taskBase = currencyRepository.GetCurrencyByCode(baseCode);
        var taskTarget = currencyRepository.GetCurrencyByCode(targetCode);
        Task.WaitAll(taskBase, taskTarget);
        var baseCurrency = taskBase.Result!;
        var targetCurrency = taskTarget.Result!;
        
        return new ExchangeResponse(
            mapper.Map<CurrencyResponse>(baseCurrency),
            mapper.Map<CurrencyResponse>(targetCurrency), 
            decimal.Round(potentialRate.Value, 4),
            amount,
            decimal.Round(potentialRate.Value * amount));
    }
}