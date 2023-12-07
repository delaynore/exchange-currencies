using CurrencyExchange.API.Models;
using CurrencyExchange.API.Repositories;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public void CreateCurrency(Currency currency)
        {
            _currencyRepository.CreateCurrency(currency);
        }

        public void DeleteCurrency(int id)
        {
            _currencyRepository.DeleteCurrency(id);
        }

        public IQueryable<Currency> GetAll()
        {
           return _currencyRepository.GetAll();
        }

        public Currency? GetCurrencyByCode(string code)
        {
            return _currencyRepository.GetCurrencyByCode(code);
        }

        public Currency? GetCurrencyById(int id)
        {
            return _currencyRepository.GetCurrencyById(id);
        }

        public void UpdateCurrency(Currency currency)
        {
            _currencyRepository.UpdateCurrency(currency);
        }
    }
}
