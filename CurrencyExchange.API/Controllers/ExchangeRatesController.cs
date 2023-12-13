using CurrencyExchange.API.Models;
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
            return Ok(_exchangeRateService.GetAll());
        }

        [HttpGet("{basetarget}")]
        public IActionResult GetExchangeRate(string basetarget) 
        {
            if (basetarget is null) return  BadRequest();

            if (basetarget.Length != 6) return BadRequest();

            var baseCurrency = basetarget[..3].ToUpper();
            var targetCurrency = basetarget[3..].ToUpper();

            var exchangerate = _exchangeRateService.GetByCodes(baseCurrency, targetCurrency);
            
            if(exchangerate is null) return NotFound();

            return Ok(_exchangeRateService.GetByCodes(baseCurrency, targetCurrency));
        }

        [HttpPost]
        public IActionResult CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            if (newExchangeRate.BaseCurrencyId.Equals(newExchangeRate.TargetCurrencyId))
            {
                ModelState.AddModelError("Code", "Base and Target must be different"); 
                //return BadRequest();
            }
            _exchangeRateService.Create(newExchangeRate);

            return Created();
        }
    }
}
