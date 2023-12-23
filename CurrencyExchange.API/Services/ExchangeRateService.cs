using AutoMapper;
using AutoMapper.QueryableExtensions;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Response;
using CurrencyExchange.API.Errors;

namespace CurrencyExchange.API.Services
{
    public class ExchangeRateService
        (IExchangeRateRepository exchangeRateRepository, 
        ICurrencyRepository currencyRepository,
        IMapper mapper) : IExchangeRateService
    {

        private readonly IExchangeRateRepository _exchangeRateRepository = exchangeRateRepository;
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;
        private readonly IMapper _mapper = mapper;

        public Result<ExchangeRateResponse> Create(ExchangeRateRequest exchangeRate)
        {
            if (_currencyRepository.GetCurrencyById(exchangeRate.TargetCurrencyId) is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.CurrencyErrors.NotFound(exchangeRate.TargetCurrencyId));

            if (_currencyRepository.GetCurrencyById(exchangeRate.BaseCurrencyId) is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.CurrencyErrors.NotFound(exchangeRate.BaseCurrencyId));

            if (exchangeRate.BaseCurrencyId == exchangeRate.TargetCurrencyId)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.SameCurrencies());

            var rateToCreate = _mapper.Map<ExchangeRate>(exchangeRate);
            try
            {
                _exchangeRateRepository.Create(rateToCreate);
                return _mapper.Map<ExchangeRateResponse>(_exchangeRateRepository.GetById(rateToCreate.Id));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result Delete(int id)
        {
            try
            {
                _exchangeRateRepository.Delete(id);
                return Result.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Result<List<ExchangeRateResponse>> GetAll()
        {
            return _exchangeRateRepository
                .GetAll()
                .ProjectTo<ExchangeRateResponse>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public Result<ExchangeRateResponse> GetByCodes(string baseCode, string targetCode)
        {
            if (baseCode is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(baseCode)));
            if (targetCode is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NullValue(nameof(targetCode)));
            if (baseCode.Length != 3)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("base"));
            if (targetCode.Length != 3)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.InvalidLength("target"));


            var exchangeRate = _exchangeRateRepository.GetByCodes(baseCode, targetCode);
            if (exchangeRate is null) 
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound(baseCode, targetCode));
            return _mapper.Map<ExchangeRateResponse>(exchangeRate);
        }

        public Result<ExchangeRateResponse> GetById(int id)
        {
            var exchangeRate = _exchangeRateRepository.GetById(id);
            if (exchangeRate is null)
                return Result.Failure<ExchangeRateResponse>(ApplicationErrors.ExchangeRateErrors.NotFound(id));
            return _mapper.Map<ExchangeRateResponse>(exchangeRate);
        }

        public Result Update(int id, ExchangeRateRequest exchangeRate)
        {
            var exchangeRateToUpdate = _exchangeRateRepository.GetById(id);
            if (exchangeRateToUpdate is null) 
                return Result.Failure(ApplicationErrors.ExchangeRateErrors.NotFound(id));

            exchangeRateToUpdate.Rate = exchangeRate.Rate;
            exchangeRateToUpdate.TargetCurrencyId = exchangeRate.TargetCurrencyId;
            exchangeRateToUpdate.BaseCurrencyId = exchangeRate.BaseCurrencyId;

            try
            {
                _exchangeRateRepository.Update(exchangeRateToUpdate);
                return Result.Success();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
