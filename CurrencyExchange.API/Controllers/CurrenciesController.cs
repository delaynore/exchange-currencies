using CurrencyExchange.API.Models.Contracts.Currency;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    public class CurrenciesController(ICurrencyService currencyService) : ApiBaseController
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
            if (result.IsFailure) return BadRequest(result.Error);
            return Created();
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            var check = _currencyService.GetCurrencyById(id);
            if (check.IsFailure) return BadRequest(check.IsFailure);

            var result = _currencyService.UpdateCurrency(id, updateCurrency);
            if (result.IsFailure) return BadRequest(result.Error);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCurrency(int id)
        {
            var result = _currencyService.DeleteCurrency(id);
            if (result.IsFailure) return BadRequest(result.Error);
            return NoContent();
        }

    }
}
