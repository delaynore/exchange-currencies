using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ApiBaseController
    {
        private readonly IExchangeRateService _exchangeRateService = exchangeRateService;

        [HttpGet]
        public IActionResult GetAllExchangeRates()
        {
            var result = _exchangeRateService.GetAll();
            if (result.IsFailure ) return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{baseCurrency}-{targetCurrency}")]
        public IActionResult GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var result = _exchangeRateService.GetByCodes(baseCurrency, targetCurrency);
            if (result.IsFailure) return NotFound();
            return Ok(result.Value);
        }

        [HttpPost]
        public IActionResult CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            var result = _exchangeRateService.Create(newExchangeRate);
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

        [HttpDelete("{id:int}")]
        public IActionResult DeleteExchangeRate(int id)
        {
            var result = _exchangeRateService.Delete(id);
            return result.IsSuccess ? NoContent() : BadRequest(result.Error);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateExchangeRate(int id, ExchangeRateRequest exchangeRateRequest)
        {
            var result = _exchangeRateService.Update(id, exchangeRateRequest);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
