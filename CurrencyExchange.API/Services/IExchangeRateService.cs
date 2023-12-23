using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Response;


namespace CurrencyExchange.API.Services
{
    public interface IExchangeRateService
    {
        Task<Result<List<ExchangeRateResponse>>> GetAll();
        Task<Result<ExchangeRateResponse>> GetById(int id);
        Task<Result<ExchangeRateResponse>> GetByCodes(string baseCode, string targetCode);

        Task<Result<ExchangeRateResponse>> Create(ExchangeRateRequest exchangeRate);
        Task<Result> Update(int id, ExchangeRateRequest exchangeRate);
        Task<Result> Delete(int id);
    }
}
