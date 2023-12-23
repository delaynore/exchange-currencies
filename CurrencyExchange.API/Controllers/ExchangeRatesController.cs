using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllExchangeRates()
        {
            var result = await exchangeRateService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpGet("{baseCurrency}-{targetCurrency}")]
        public async Task<IActionResult> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var result = await exchangeRateService.GetByCodes(baseCurrency, targetCurrency);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            var result = await exchangeRateService.Create(newExchangeRate);
            return result.IsSuccess 
                ? CreatedAtAction(
                    nameof(GetExchangeRate), 
                    new
                    {
                        baseCurrency = result.Value.BaseCurrency.Code, 
                        targetCurrency = result.Value.TargetCurrency.Code
                    },
                    result.Value) 
                : BadRequest(result.Error);
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateExchangeRate(int id, ExchangeRateRequest exchangeRateRequest)
        {
            var result = await exchangeRateService.Update(id, exchangeRateRequest);
            return result.IsSuccess 
                ? Ok() 
                : BadRequest(result.Error);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteExchangeRate(int id)
        {
            var result = await exchangeRateService.Delete(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error);
        }
    }

