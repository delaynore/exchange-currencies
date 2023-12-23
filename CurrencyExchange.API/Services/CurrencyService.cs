using AutoMapper;
using AutoMapper.QueryableExtensions;
using CurrencyExchange.API.Errors;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepository, IMapper mapper) : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;
        private readonly IMapper _mapper = mapper;
        public Result<CurrencyResponse> CreateCurrency(CurrencyRequest currency)
        {
            var currencyToCreate = _mapper.Map<Currency>(currency);
            currencyToCreate.Code = currencyToCreate.Code.ToUpper();
            if (_currencyRepository.GetCurrencyByCode(currencyToCreate.Code) is not null)
                return Result.Failure<CurrencyResponse>(ApplicationErrors.CurrencyErrors.AlreadyExists(currency.Code));

            _currencyRepository.CreateCurrency(currencyToCreate);
            return _mapper.Map<CurrencyResponse>(currencyToCreate);
        }

        public Result DeleteCurrency(int id)
        {
            _currencyRepository.DeleteCurrency(id);
            return Result.Success();
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

            _currencyRepository.UpdateCurrency(currencyToUpdate);
            return Result.Success();
        }
    }
}
