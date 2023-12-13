using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;

namespace CurrencyExchange.API.Services
{
    public interface ICurrencyService
    {
        IQueryable<CurrencyResponse> GetAll();
        CurrencyResponse? GetCurrencyById(int id);
        CurrencyResponse? GetCurrencyByCode(string code);
        void CreateCurrency(CurrencyRequest currency);
        void UpdateCurrency(int id, CurrencyRequest currency);
        void DeleteCurrency(int id);
    }
}
