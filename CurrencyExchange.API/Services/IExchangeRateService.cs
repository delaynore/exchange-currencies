using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Response;


namespace CurrencyExchange.API.Services
{
    public interface IExchangeRateService
    {
        Result<List<ExchangeRateResponse>> GetAll();
        Result<ExchangeRateResponse> GetById(int id);
        Result<ExchangeRateResponse> GetByCodes(string baseCode, string targetCode);

        Result<ExchangeRateResponse> Create(ExchangeRateRequest exchangeRate);
        Result Update(int id, ExchangeRateRequest exchangeRate);
        Result Delete(int id);
    }
}
