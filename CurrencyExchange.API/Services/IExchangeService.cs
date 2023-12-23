using CurrencyExchange.API.Contracts.Exchange;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services;

public interface IExchangeService
{
    Task<Result<ExchangeResponse>> Exchange(string baseCode, string targetCode, decimal amount);
}