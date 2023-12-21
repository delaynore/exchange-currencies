using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        public Result<int> CreateCurrency(CurrencyRequest currency)
        {
            if (_currencyRepository.GetCurrencyByCode(currency.Code) is not null)
                return Result.Failure<int>(ApplicationErrors.CurrencyErrors.AlreadyExists);

            var currencyToCreate = new Currency()
            {
                FullName = currency.FullName,
                Code = currency.Code,
                Sign = currency.Sign,
            };
            
            _currencyRepository.CreateCurrency(currencyToCreate);
            return currencyToCreate.Id;
        }

        public Result DeleteCurrency(int id)
        {
            try
            {
                _currencyRepository.DeleteCurrency(id);
                return Result.Success();
            }
            catch (Exception)
            {
                throw;
                //return Result.Failure(new Error("Global.DeleteException"));
            }
        }

        public Result<List<CurrencyResponse>> GetAll()
        {
           return _currencyRepository
                .GetAll()
                .Select(d => new CurrencyResponse(
                    d.Id,
                    d.Code,
                    d.FullName,
                    d.Sign))
                .ToList();
        }

        public Result<CurrencyResponse> GetCurrencyByCode(string code)
        {
            var currency = _currencyRepository.GetCurrencyByCode(code);
            if (currency is null) 
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.ExchangeRateErrors.NotFound);

            return new CurrencyResponse(
                currency.Id,
                currency.Code,
                currency.FullName,
                currency.Sign);
        }

        public Result<CurrencyResponse> GetCurrencyById(int id)
        {
            var currency = _currencyRepository.GetCurrencyById(id);
            if (currency is null)
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.ExchangeRateErrors.NotFound);

            return new CurrencyResponse(
                currency.Id,
                currency.Code,
                currency.FullName,
                currency.Sign);
        }

        public Result UpdateCurrency(int id, CurrencyRequest currency)
        {
            var currencyToUpdate = _currencyRepository.GetCurrencyById(id);
            if (currencyToUpdate is null)
                return ApplicationErrors.ExchangeRateErrors.NotFound;

            currencyToUpdate.Code = currency.Code;
            currencyToUpdate.FullName = currency.FullName;
            currencyToUpdate.Sign = currency.Sign;

            try
            {
                _currencyRepository.UpdateCurrency(currencyToUpdate);
            }
            catch (Exception)
            {
                throw;
            }
            return Result.Success();
        }
    }
}
