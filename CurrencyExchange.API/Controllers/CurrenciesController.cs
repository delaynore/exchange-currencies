using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    public class CurrenciesController(ICurrencyService currencyService) : ApiBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result = await currencyService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetCurrency(string code)
        {
            var result = await currencyService.GetCurrencyByCode(code);
            return result.IsSuccess 
                ? Ok(result.Value)
                : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrency(CurrencyRequest newCurrency)
        {
            var result = await currencyService.CreateCurrency(newCurrency);
            return result.IsSuccess 
                ? CreatedAtAction(
                    nameof(GetCurrency), 
                    new { result.Value.Code }, 
                    result.Value) 
                : BadRequest(result.Error);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            var result = await currencyService.UpdateCurrency(id, updateCurrency);
            return result.IsSuccess 
                ? Ok()
                : BadRequest(result.Error);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = await currencyService.DeleteCurrency(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error) ;
        }

    }
}
