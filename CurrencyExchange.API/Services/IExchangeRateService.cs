using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Response;


namespace CurrencyExchange.API.Services
{
    public interface IExchangeRateService
    {
        Result<List<ExchangeRateResponse>> GetAll();
        Result<ExchangeRateResponse> GetById(int id);
        Result<ExchangeRateResponse> GetByCodes(string baseCode, string targetCode);

        Result<int> Create(ExchangeRateRequest exchangeRate);
        Result Update(int id, ExchangeRateRequest exchangeRate);
        Result Delete(int id);
    }
}
