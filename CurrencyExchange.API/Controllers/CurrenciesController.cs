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
            var result = _currencyService.GetAll();
            if(result.IsFailure) return NotFound();
            return Ok(result.Value);
        }

        [HttpGet("{code}")]
        public IActionResult GetCurrency(string code)
        {
            if (code.Length != 3) return BadRequest(new { message = $"Invalid code of currency - {code}" });

            var currency = _currencyService.GetCurrencyByCode(code.ToUpper());
            if (currency.IsFailure) return NotFound();

            return Ok(currency);
        }

        [HttpPost]
        public IActionResult CreateCurrency(CurrencyRequest newCurrency)
        {
            var result = _currencyService.CreateCurrency(newCurrency);
            if (result.IsFailure) return BadRequest();
            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            if (_currencyService.GetCurrencyById(id).IsFailure) return BadRequest();

            var result = _currencyService.UpdateCurrency(id, updateCurrency);
            if (result.IsFailure) return BadRequest();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCurrency(int id)
        {
            var result = _currencyService.DeleteCurrency(id);
            if (result.IsFailure) return BadRequest();
            return NoContent();
        }

    }
}
