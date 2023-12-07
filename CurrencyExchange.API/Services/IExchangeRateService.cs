using CurrencyExchange.API.Models;

namespace CurrencyExchange.API.Services
{
    public interface IExchangeRateService
    {
        IQueryable<ExchangeRate> GetAll();
        ExchangeRate? GetById(int id);
        ExchangeRate? GetByCodes(string baseCode, string targetCode);

        void Create(ExchangeRate rate);
        void Update(ExchangeRate rate);
        void Delete(int id);
    }
}
