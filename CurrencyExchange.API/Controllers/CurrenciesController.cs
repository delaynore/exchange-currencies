using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    public class CurrenciesController(ICurrencyService currencyService) : ApiBaseController
    {
        [HttpGet]
        public IActionResult GetAllCurrencies()
        {
            var result = currencyService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpGet("{code}")]
        public IActionResult GetCurrency(string code)
        {
            var result = currencyService.GetCurrencyByCode(code);
            return result.IsSuccess 
                ? Ok(result.Value)
                : NotFound();
        }

        [HttpPost]
        public IActionResult CreateCurrency(CurrencyRequest newCurrency)
        {
            var result = currencyService.CreateCurrency(newCurrency);
            return result.IsSuccess 
                ? CreatedAtAction(
                    nameof(GetCurrency), 
                    new { result.Value.Code }, 
                    result.Value) 
                : BadRequest(result.Error);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            var result = currencyService.UpdateCurrency(id, updateCurrency);
            return result.IsSuccess 
                ? Ok()
                : BadRequest(result.Error);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCurrency(int id)
        {
            var result = currencyService.DeleteCurrency(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error) ;
        }

    }
}
