using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Repositories;

namespace CurrencyExchange.API.Services
{
    public class ExchangeRateService(IExchangeRateRepository exchangeRateRepository) : IExchangeRateService
    {

        private readonly IExchangeRateRepository _exchangeRateRepository = exchangeRateRepository;

        public void Create(ExchangeRateRequest exchangeRate)
        {
            var rateToCreate = new ExchangeRate()
            {
                BaseCurrencyId = exchangeRate.BaseCurrencyId,
                TargetCurrencyId = exchangeRate.TargetCurrencyId,
                Rate = exchangeRate.Rate
            };

            _exchangeRateRepository.Create(rateToCreate);
        }

        public void Delete(int id)
        {
            _exchangeRateRepository.Delete(id);
        }

        public List<ExchangeRateResponse> GetAll()
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

        public ExchangeRateResponse? GetByCodes(string baseCode, string targetCode)
        {
            var exchangeRate = _exchangeRateRepository.GetByCodes(baseCode, targetCode);
            if (exchangeRate is null) return null;
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

        public ExchangeRateResponse? GetById(int id)
        {
            var exchangeRate = _exchangeRateRepository.GetById(id);
            if (exchangeRate is null) return null;
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

        public void Update(int id, ExchangeRateRequest exchangeRate)
        {
            var exchangeRateToUpdate = _exchangeRateRepository.GetById(id);
            if (exchangeRateToUpdate is null) return; ///?????

            exchangeRateToUpdate.Rate = exchangeRate.Rate;
            exchangeRateToUpdate.TargetCurrencyId = exchangeRate.TargetCurrencyId;
            exchangeRateToUpdate.BaseCurrencyId = exchangeRate.BaseCurrencyId;

            _exchangeRateRepository.Update(exchangeRateToUpdate);
        }
    }
}
