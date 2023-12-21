using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService = exchangeRateService;

        [HttpGet]
        public IActionResult GetAllExchangeRates()
        {
            var result = _exchangeRateService.GetAll();
            if (result.IsFailure ) return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{basetarget}")]
        public IActionResult GetExchangeRate(string basetarget)
        {
            if (basetarget is null) return BadRequest();
            if (basetarget.Length != 6) return BadRequest();

            var baseCurrency = basetarget[..3].ToUpper();
            var targetCurrency = basetarget[3..].ToUpper();

            var result = _exchangeRateService.GetByCodes(baseCurrency, targetCurrency);

            if (result.IsFailure) return NotFound();

            return Ok(result.Value);
        }

        [HttpPost]
        public IActionResult CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            var result = _exchangeRateService.Create(newExchangeRate);
            return result.IsSuccess ? Created() : BadRequest();
        }

        [HttpDelete("{id?}")]
        public IActionResult DeleteExchangeRate(int? id)
        {
            if (id == null || id.Value < 0)
            {
                ModelState.AddModelError(nameof(id), "Parameter can't be null");
                return BadRequest(ModelState);
            }
            var result = _exchangeRateService.Delete(id.Value);
            return result.IsSuccess ? NoContent() : BadRequest();
        }

        [HttpPut("{id?}")]
        public IActionResult UpdateExchangeRate(int? id, ExchangeRateRequest exchangeRateRequest)
        {
            if (id is null) return BadRequest(ModelState);

            var result = _exchangeRateService.Update(id.Value, exchangeRateRequest);

            return result.IsSuccess ? Ok() : BadRequest();
        }
    }
}
