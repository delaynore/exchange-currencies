using System.Collections;
using AutoMapper;
using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;

namespace CurrencyExchange.API.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CurrencyRequest, Currency>();
        CreateMap<Currency, CurrencyResponse>()
            .ForCtorParam(nameof(CurrencyResponse.CurrencyId), 
                opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(CurrencyResponse.FullName), 
                opt => opt.MapFrom(src => src.FullName))
            .ForCtorParam(nameof(CurrencyResponse.Code), 
                opt => opt.MapFrom(src => src.Code))
            .ForCtorParam(nameof(CurrencyResponse.Sign), 
                opt => opt.MapFrom(src => src.Sign));

        CreateMap<ExchangeRateRequest, ExchangeRate>();
        CreateMap<ExchangeRate, ExchangeRateResponse>()
            .ForCtorParam(nameof(ExchangeRateResponse.Rate),
                opt => opt.MapFrom(src => src.Rate))
            .ForCtorParam(nameof(ExchangeRateResponse.ExchangeRateId),
                opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(ExchangeRateResponse.BaseCurrency),
                opt => opt.MapFrom(src => src.BaseCurrency))
            .ForCtorParam(nameof(ExchangeRateResponse.TargetCurrency),
                opt => opt.MapFrom(src => src.TargetCurrency));
    }
}