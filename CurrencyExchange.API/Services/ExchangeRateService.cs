using CurrencyExchange.API.Models;
using CurrencyExchange.API.Repositories;

namespace CurrencyExchange.API.Services
{
    public class ExchangeRateService(IExchangeRateRepository exchangeRateRepository) : IExchangeRateService
    {

        private readonly IExchangeRateRepository _exchangeRateRepository = exchangeRateRepository;

        public void Create(ExchangeRate rate)
        {
           _exchangeRateRepository.Create(rate);
        }

        public void Delete(int id)
        {
            _exchangeRateRepository.Delete(id);
        }

        public IQueryable<ExchangeRate> GetAll()
        {
            return _exchangeRateRepository.GetAll();
        }

        public ExchangeRate? GetByCodes(string baseCode, string targetCode)
        {
            return _exchangeRateRepository.GetByCodes(baseCode, targetCode);
        }

        public ExchangeRate? GetById(int id)
        {
           return _exchangeRateRepository.GeteById(id);
        }

        public void Update(ExchangeRate rate)
        {
            _exchangeRateRepository.Update(rate);
        }
    }
}
