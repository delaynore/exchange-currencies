using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Repositories;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public void CreateCurrency(CurrencyRequest currency)
        {
            var currencyToCreate = new Currency()
            {
                FullName = currency.FullName,
                Code = currency.Code,
                Sign = currency.Sign,
            };

            _currencyRepository.CreateCurrency(currencyToCreate);
        }

        public void DeleteCurrency(int id)
        {
            _currencyRepository.DeleteCurrency(id);
        }

        public IQueryable<CurrencyResponse> GetAll()
        {
           return _currencyRepository
                .GetAll()
                .Select(d => new CurrencyResponse(
                    d.Id,
                    d.Code,
                    d.FullName,
                    d.Sign));
        }

        public CurrencyResponse? GetCurrencyByCode(string code)
        {
            var currency = _currencyRepository.GetCurrencyByCode(code);
            if (currency is null) return null;

            return new CurrencyResponse(
                currency.Id,
                currency.Code,
                currency.FullName,
                currency.Sign);
        }

        public CurrencyResponse? GetCurrencyById(int id)
        {
            var currency = _currencyRepository.GetCurrencyById(id);
            if (currency is null) return null;

            return new CurrencyResponse(
                currency.Id,
                currency.Code,
                currency.FullName,
                currency.Sign);
        }

        public void UpdateCurrency(int id, CurrencyRequest currency)
        {
            var currencyToUpdate = _currencyRepository.GetCurrencyById(id);
            if(currencyToUpdate is null) return;

            currencyToUpdate.Code = currency.Code;
            currencyToUpdate.FullName = currency.FullName;
            currencyToUpdate.Sign = currency.Sign;

            _currencyRepository.UpdateCurrency(currencyToUpdate);
        }
    }
}
