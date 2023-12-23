﻿using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Response;

namespace CurrencyExchange.API.Services
{
    public interface ICurrencyService
    {
        Result<List<CurrencyResponse>> GetAll();
        Result<CurrencyResponse> GetCurrencyById(int id);
        Result<CurrencyResponse> GetCurrencyByCode(string code);
        Result<CurrencyResponse> CreateCurrency(CurrencyRequest currency);
        Result UpdateCurrency(int id, CurrencyRequest currency);
        Result DeleteCurrency(int id);
    }
}
