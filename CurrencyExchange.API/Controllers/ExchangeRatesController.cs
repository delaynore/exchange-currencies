using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ApiBaseController
    {
        [HttpGet]
        public IActionResult GetAllExchangeRates()
        {
            var result = exchangeRateService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpGet("{baseCurrency}-{targetCurrency}")]
        public IActionResult GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var result = exchangeRateService.GetByCodes(baseCurrency, targetCurrency);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpPost]
        public IActionResult CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            var result = exchangeRateService.Create(newExchangeRate);
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
        public IActionResult UpdateExchangeRate(int id, ExchangeRateRequest exchangeRateRequest)
        {
            var result = exchangeRateService.Update(id, exchangeRateRequest);
            return result.IsSuccess 
                ? Ok() 
                : BadRequest(result.Error);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteExchangeRate(int id)
        {
            var result = exchangeRateService.Delete(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error);
        }
    }
}
