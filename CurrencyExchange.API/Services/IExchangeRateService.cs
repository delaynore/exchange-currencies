using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;


namespace CurrencyExchange.API.Services
{
    public interface IExchangeRateService
    {
        IQueryable<ExchangeRateResponse> GetAll();
        ExchangeRateResponse? GetById(int id);
        ExchangeRateResponse? GetByCodes(string baseCode, string targetCode);

        void Create(ExchangeRateRequest exchangeRate);
        void Update(int id, ExchangeRateRequest exchangeRate);
        void Delete(int id);
    }
}
