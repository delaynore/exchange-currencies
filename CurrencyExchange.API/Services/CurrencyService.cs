﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper) : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;
        private readonly IMapper _mapper = mapper;
        public Result<int> CreateCurrency(CurrencyRequest currency)
        {
            if (_currencyRepository.GetCurrencyByCode(currency.Code) is not null)
                return Result.Failure<int>(ApplicationErrors.CurrencyErrors.AlreadyExists(currency.Code));

            var currencyToCreate = _mapper.Map<Currency>(currency);
            
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
                .ProjectTo<CurrencyResponse>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public Result<CurrencyResponse> GetCurrencyByCode(string code)
        {
            if (code.Length != 3)
                return Result.Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.InvalidLength());
            
            var currency = _currencyRepository.GetCurrencyByCode(code);
            if (currency is null) 
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.NotFound(code));
            return _mapper.Map<CurrencyResponse>(currency);
        }

        public Result<CurrencyResponse> GetCurrencyById(int id)
        {
            var currency = _currencyRepository.GetCurrencyById(id);
            if (currency is null)
                return Result
                    .Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.NotFound(id));
            return _mapper.Map<CurrencyResponse>(currency);
        }

        public Result UpdateCurrency(int id, CurrencyRequest currency)
        {
            var currencyToUpdate = _currencyRepository.GetCurrencyById(id);
            if (currencyToUpdate is null)
                return ApplicationErrors.CurrencyErrors.NotFound(id);

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
