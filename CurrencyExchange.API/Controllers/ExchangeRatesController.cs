using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.ExchangeRate;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Runtime.Serialization;

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
            return Ok(_exchangeRateService.GetAll());
        }

        [HttpGet("{basetarget}")]
        public IActionResult GetExchangeRate(string basetarget)
        {
            if (basetarget is null) return BadRequest();

            if (basetarget.Length != 6) return BadRequest();

            var baseCurrency = basetarget[..3].ToUpper();
            var targetCurrency = basetarget[3..].ToUpper();

            var exchangerate = _exchangeRateService.GetByCodes(baseCurrency, targetCurrency);

            if (exchangerate is null) return NotFound();

            return Ok(_exchangeRateService.GetByCodes(baseCurrency, targetCurrency));
        }

        [HttpPost]
        public IActionResult CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            if (newExchangeRate.BaseCurrencyId.Equals(newExchangeRate.TargetCurrencyId))
            {
                ModelState.AddModelError("Code", "Base and Target must be different");
                return BadRequest(ModelState);
            }
            _exchangeRateService.Create(newExchangeRate);

            return Created();
        }

        [HttpDelete("{id?}")]
        public IActionResult DeleteExchangeRate(int? id)
        {
            if (id == null || id.Value < 0)
            {
                ModelState.AddModelError(nameof(id), "Parameter can't be null");
                return BadRequest(ModelState);
            }
            _exchangeRateService.Delete(id.Value);

            return NoContent();
        }

        [HttpPut("{id?}")]
        public IActionResult UpdateExchangeRate(int? id, ExchangeRateRequest exchangeRateRequest)
        {
            if (id is null) return BadRequest(ModelState);

            _exchangeRateService.Update(id.Value, exchangeRateRequest);

            return Ok();
        }
    }
}
