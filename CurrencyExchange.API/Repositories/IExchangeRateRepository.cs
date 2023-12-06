using CurrencyExchange.API.Models;

namespace CurrencyExchange.API.Repositories
{
    public interface IExchangeRateRepository
    {
        IQueryable<ExchangeRate> GetAll();
        ExchangeRate? GeteById(int id);
        ExchangeRate? GetByCodes(string baseCode, string targetCode);

        void Create(ExchangeRate rate);
        void Update(ExchangeRate rate);
        void Delete(int id);
    }
}
