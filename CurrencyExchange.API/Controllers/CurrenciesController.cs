using CurrencyExchange.API.Models;
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
            if(code.Length != 3) return BadRequest(new { message = $"Invalid code of currency - {code}" });

            var currency = _currencyService.GetCurrencyByCode(code.ToUpper());
            if (currency is null) return NotFound();

            return Ok(currency);
        }

        [HttpPost]
        public IActionResult CreateCurrency(Currency newCurrency)
        {
            _currencyService.CreateCurrency(newCurrency);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCurrency(Currency updateCurrency)
        {
            var currency = _currencyService.GetCurrencyById(updateCurrency.Id);
            if (currency is null) return BadRequest();

            //вынести в сервис
            currency.FullName = updateCurrency.FullName;
            currency.Code = updateCurrency.Code;
            currency.Sign = updateCurrency.Sign;
            _currencyService.UpdateCurrency(currency);
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
