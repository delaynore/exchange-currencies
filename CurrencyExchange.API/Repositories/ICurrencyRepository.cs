using CurrencyExchange.API.Models;

namespace CurrencyExchange.API.Repositories
{
    public interface ICurrencyRepository
    {
        IQueryable<Currency> GetAll();
        Task<Currency?> GetCurrencyById(int id);
        Task<Currency?> GetCurrencyByCode(string code);
        Task CreateCurrency(Currency currency);
        Task UpdateCurrency(Currency currency);
        Task DeleteCurrency(int id);
    }
}
