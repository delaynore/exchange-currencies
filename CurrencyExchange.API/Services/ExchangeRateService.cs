using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;
using CurrencyExchange.API.Errors;

namespace CurrencyExchange.API.Services
{
    public class ExchangeRateService
        (IExchangeRateRepository exchangeRateRepository, 
        ICurrencyRepository currencyRepository) : IExchangeRateService
    {

        private readonly IExchangeRateRepository _exchangeRateRepository = exchangeRateRepository;
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public Result<int> Create(ExchangeRateRequest exchangeRate)
        {
            if (_currencyRepository.GetCurrencyById(exchangeRate.TargetCurrencyId) is null)
                return Result.Failure<int>(ApplicationErrors.ExchangeRateErrors.NotFound);

            if (_currencyRepository.GetCurrencyById(exchangeRate.BaseCurrencyId) is null)
                return Result.Failure<int>(ApplicationErrors.ExchangeRateErrors.NotFound);

            if (exchangeRate.BaseCurrencyId == exchangeRate.TargetCurrencyId)
                return Result.Failure<int>(ApplicationErrors.ExchangeRateErrors.SameCurrencies);

            var rateToCreate = new ExchangeRate()
            {
                BaseCurrencyId = exchangeRate.BaseCurrencyId,
                TargetCurrencyId = exchangeRate.TargetCurrencyId,
                Rate = exchangeRate.Rate
            };
            try
            {
                _exchangeRateRepository.Create(rateToCreate);
                return rateToCreate.Id;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result Delete(int id)
        {
            try
            {
                _exchangeRateRepository.Delete(id);
                return Result.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<List<ExchangeRateResponse>> GetAll()
        {
            return _exchangeRateRepository
                .GetAll()
                .Select(d => new ExchangeRateResponse(
                    d.Id,
                    new CurrencyResponse(
                        d.BaseCurrencyId, 
                        d.BaseCurrency.Code, 
                        d.BaseCurrency.FullName, 
                        d.BaseCurrency.Sign),
                    new CurrencyResponse(
                        d.TargetCurrencyId,
                        d.TargetCurrency.Code, 
                        d.TargetCurrency.FullName, 
                        d.TargetCurrency.Sign),
                    d.Rate))
                .ToList();
        }

        public Result<ExchangeRateResponse> GetByCodes(string baseCode, string targetCode)
        {
            var exchangeRate = _exchangeRateRepository.GetByCodes(baseCode, targetCode);
            if (exchangeRate is null) 
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound);
            
            return new ExchangeRateResponse(exchangeRate.Id,
                    new CurrencyResponse(
                        exchangeRate.BaseCurrencyId,
                        exchangeRate.BaseCurrency.Code,
                        exchangeRate.BaseCurrency.FullName,
                        exchangeRate.BaseCurrency.Sign),
                    new CurrencyResponse(
                        exchangeRate.TargetCurrencyId,
                        exchangeRate.TargetCurrency.Code,
                        exchangeRate.TargetCurrency.FullName,
                        exchangeRate.TargetCurrency.Sign),
                    exchangeRate.Rate);
        }

        public Result<ExchangeRateResponse> GetById(int id)
        {
            var exchangeRate = _exchangeRateRepository.GetById(id);
            if (exchangeRate is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound);
            
            return new ExchangeRateResponse(exchangeRate.Id,
                    new CurrencyResponse(
                        exchangeRate.BaseCurrencyId,
                        exchangeRate.BaseCurrency.Code,
                        exchangeRate.BaseCurrency.FullName,
                        exchangeRate.BaseCurrency.Sign),
                    new CurrencyResponse(
                        exchangeRate.TargetCurrencyId,
                        exchangeRate.TargetCurrency.Code,
                        exchangeRate.TargetCurrency.FullName,
                        exchangeRate.TargetCurrency.Sign),
                    exchangeRate.Rate);
        }

        public Result Update(int id, ExchangeRateRequest exchangeRate)
        {
            var exchangeRateToUpdate = _exchangeRateRepository.GetById(id);
            if (exchangeRateToUpdate is null) 
                return Result.Failure(ApplicationErrors.ExchangeRateErrors.NotFound);

            exchangeRateToUpdate.Rate = exchangeRate.Rate;
            exchangeRateToUpdate.TargetCurrencyId = exchangeRate.TargetCurrencyId;
            exchangeRateToUpdate.BaseCurrencyId = exchangeRate.BaseCurrencyId;

            try
            {
                _exchangeRateRepository.Update(exchangeRateToUpdate);
                return Result.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
