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
            var result = _currencyService.GetCurrencyByCode(code.ToUpper());
            if (result.IsFailure) return NotFound();
            return Ok(result.Value);
        }

        [HttpPost]
        public IActionResult CreateCurrency(CurrencyRequest newCurrency)
        {
            var result = _currencyService.CreateCurrency(newCurrency);
            return result.IsSuccess ? Created() : BadRequest(result.Error);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
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
