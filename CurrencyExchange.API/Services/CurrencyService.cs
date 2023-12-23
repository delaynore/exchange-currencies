using AutoMapper;
using AutoMapper.QueryableExtensions;
using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper) : ICurrencyService
    {
        public async Task<Result<CurrencyResponse>> CreateCurrency(CurrencyRequest currency)
        {
            var currencyToCreate = mapper.Map<Currency>(currency);
            currencyToCreate.Code = currencyToCreate.Code.ToUpper();
            if (await currencyRepository.GetCurrencyByCode(currencyToCreate.Code) is not null)
                return Result.Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.AlreadyExists(currency.Code));

            await currencyRepository.CreateCurrency(currencyToCreate);
            return mapper.Map<CurrencyResponse>(currencyToCreate);
        }

        public async Task<Result> DeleteCurrency(int id)
        {
            await currencyRepository.DeleteCurrency(id);
            return Result.Success();
        }

        public async Task<Result<List<CurrencyResponse>>> GetAll()
        {
            return await currencyRepository
                .GetAll()
                .ProjectTo<CurrencyResponse>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<Result<CurrencyResponse>> GetCurrencyByCode(string code)
        {
            if (code.Length != 3)
                return Result.Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.InvalidLength());
            
            var currency = await currencyRepository.GetCurrencyByCode(code);
            if (currency is null) 
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.NotFound(code));
            return mapper.Map<CurrencyResponse>(currency);
        }

        public async Task<Result<CurrencyResponse>> GetCurrencyById(int id)
        {
            var currency = await currencyRepository.GetCurrencyById(id);
            if (currency is null)
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.NotFound(id));
            return mapper.Map<CurrencyResponse>(currency);
        }

        public async Task<Result> UpdateCurrency(int id, CurrencyRequest currency)
        {
            var currencyToUpdate = await currencyRepository.GetCurrencyById(id);
            if (currencyToUpdate is null)
                return ApplicationErrors.CurrencyErrors.NotFound(id);

            currencyToUpdate.Code = currency.Code;
            currencyToUpdate.FullName = currency.FullName;
            currencyToUpdate.Sign = currency.Sign;

            await currencyRepository.UpdateCurrency(currencyToUpdate);
            return Result.Success();
        }
    }
}
