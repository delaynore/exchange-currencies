using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services
{
    public interface ICurrencyService
    {
        Task<Result<List<CurrencyResponse>>> GetAll();
        Task<Result<CurrencyResponse>> GetCurrencyById(int id);
        Task<Result<CurrencyResponse>> GetCurrencyByCode(string code);
        Task<Result<CurrencyResponse>> CreateCurrency(CurrencyRequest currency);
        Task<Result> UpdateCurrency(int id, CurrencyRequest currency);
        Task<Result> DeleteCurrency(int id);
    }
}
