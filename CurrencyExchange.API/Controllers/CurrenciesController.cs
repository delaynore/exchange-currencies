using CurrencyExchange.API.Models;
using CurrencyExchange.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace CurrencyExchange.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController(ICurrencyRepository currencyRepository) : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository = currencyRepository;

        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            return Ok(_currencyRepository.GetAll());
        }

        [HttpGet("{code}")]
        public IActionResult GetCurrency(string code) 
        {
            if(code.Length != 3) return BadRequest(new { message = $"Invalid code of currency - {code}" });

            var currency = _currencyRepository.GetCurrencyByCode(code.ToUpper());
            if (currency is null) return NotFound();

            return Ok(currency);
        }

        [HttpPost]
        public IActionResult CreateCurrency(Currency newCurrency)
        {
            _currencyRepository.CreateCurrency(newCurrency);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCurrency(Currency updateCurrency)
        {
            var currency = _currencyRepository.GetCurrencyById(updateCurrency.Id);
            if (currency is null) return BadRequest();

            currency.FullName = updateCurrency.FullName;
            currency.Code = updateCurrency.Code;
            currency.Sign = updateCurrency.Sign;
            _currencyRepository.UpdateCurrency(currency);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCurrency(int id)
        {
            _currencyRepository.DeleteCurrency(id);
            return Ok();
        }

    }
}
