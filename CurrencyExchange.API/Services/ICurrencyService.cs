using CurrencyExchange.API.Models;

namespace CurrencyExchange.API.Services
{
    public interface ICurrencyService
    {
        IQueryable<Currency> GetAll();
        Currency? GetCurrencyById(int id);
        Currency? GetCurrencyByCode(string code);
        void CreateCurrency(Currency currency);
        void UpdateCurrency(Currency currency);
        void DeleteCurrency(int id);
    }
}
