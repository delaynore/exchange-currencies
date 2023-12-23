using AutoMapper;
using AutoMapper.QueryableExtensions;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;
using CurrencyExchange.API.Errors;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Services
{
    public class ExchangeRateService
        (IExchangeRateRepository exchangeRateRepository, 
        ICurrencyRepository currencyRepository,
        IMapper mapper) : IExchangeRateService
    {
        public async Task<Result<ExchangeRateResponse>> Create(ExchangeRateRequest exchangeRate)
        {
            if (exchangeRate.BaseCurrencyId == exchangeRate.TargetCurrencyId)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.SameCurrencies());
            
            var (baseCurrencyTask, targetCurrencyTask) = (
                currencyRepository.GetCurrencyById(exchangeRate.BaseCurrencyId),
                currencyRepository.GetCurrencyById(exchangeRate.TargetCurrencyId)
                );
            Task.WaitAll(baseCurrencyTask, targetCurrencyTask);
            
            if (targetCurrencyTask.Result is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.CurrencyErrors.NotFound(exchangeRate.TargetCurrencyId));

            if (baseCurrencyTask.Result is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.CurrencyErrors.NotFound(exchangeRate.BaseCurrencyId));

            var rateToCreate = mapper.Map<ExchangeRate>(exchangeRate);
            await exchangeRateRepository.Create(rateToCreate);
            return mapper.Map<ExchangeRateResponse>(exchangeRateRepository.GetById(rateToCreate.Id));
        }

        public async Task<Result> Delete(int id)
        {
            await exchangeRateRepository.Delete(id);
            return Result.Success();
        }

        public async Task<Result<List<ExchangeRateResponse>>> GetAll()
        {
            return await exchangeRateRepository
                .GetAll()
                .ProjectTo<ExchangeRateResponse>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Result<ExchangeRateResponse>> GetByCodes(string baseCode, string targetCode)
        {
            if (baseCode is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(baseCode)));
            if (targetCode is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(targetCode)));
            if (baseCode.Length != 3)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("base"));
            if (targetCode.Length != 3)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("target"));


            var exchangeRate = await exchangeRateRepository.GetByCodes(baseCode, targetCode);
            if (exchangeRate is null) 
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound(baseCode, targetCode));
            return mapper.Map<ExchangeRateResponse>(exchangeRate);
        }

        public async Task<Result<ExchangeRateResponse>> GetById(int id)
        {
            var exchangeRate = await exchangeRateRepository.GetById(id);
            if (exchangeRate is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound(id));
            return mapper.Map<ExchangeRateResponse>(exchangeRate);
        }

        public async Task<Result> Update(int id, ExchangeRateRequest exchangeRate)
        {
            var exchangeRateToUpdate = await exchangeRateRepository.GetById(id);
            if (exchangeRateToUpdate is null) 
                return Result.Failure(ApplicationErrors.ExchangeRateErrors.NotFound(id));

            exchangeRateToUpdate.Rate = exchangeRate.Rate;
            exchangeRateToUpdate.TargetCurrencyId = exchangeRate.TargetCurrencyId;
            exchangeRateToUpdate.BaseCurrencyId = exchangeRate.BaseCurrencyId;

            await exchangeRateRepository.Update(exchangeRateToUpdate);
            return Result.Success();
        }
    }
}
