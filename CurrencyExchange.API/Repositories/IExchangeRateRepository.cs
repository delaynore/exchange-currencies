using CurrencyExchange.API.Models;

namespace CurrencyExchange.API.Repositories
{
    public interface IExchangeRateRepository
    {
        IQueryable<ExchangeRate> GetAll();
        Task<ExchangeRate?> GetById(int id);
        Task<ExchangeRate?> GetByCodes(string baseCode, string targetCode);
        Task<decimal?> FindSimilarRate(string baseCode, string targetCode);
        Task Create(ExchangeRate rate);
        Task Update(ExchangeRate rate);
        Task Delete(int id);
    }
}
