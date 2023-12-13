using CurrencyExchange.API.Models;
using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController(ICurrencyService currencyService) : ControllerBase
    {
        private readonly ICurrencyService _currencyService = currencyService;

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            return Ok(_currencyService.GetAll());
        }

        [HttpGet("{code}")]
        public IActionResult GetCurrency(string code)
        {
            if (code.Length != 3) return BadRequest(new { message = $"Invalid code of currency - {code}" });

            var currency = _currencyService.GetCurrencyByCode(code.ToUpper());
            if (currency is null) return NotFound();

            return Ok(currency);
        }

        [HttpPost]
        public IActionResult CreateCurrency(CurrencyRequest newCurrency)
        {
            _currencyService.CreateCurrency(newCurrency);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            var currency = _currencyService.GetCurrencyById(id);
            if (currency is null) return BadRequest();///????

            _currencyService.UpdateCurrency(id, updateCurrency);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCurrency(int id)
        {
            _currencyService.DeleteCurrency(id);
            return Ok();
        }

    }
}
