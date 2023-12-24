using CurrencyExchange.API.Contracts.Currency;
using CurrencyExchange.API.Response;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers
{
    /// <summary>
    /// Currencies management.
    /// </summary>
    public class CurrenciesController(ICurrencyService currencyService) : ApiBaseController
    {
        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>Currencies.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllCurrencies()
        {
            var result = await currencyService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }
        
        /// <summary>
        /// Get currency by code.
        /// </summary>
        /// <param name="code">Code of currency.</param>
        /// <returns>Currency.</returns>
        [HttpGet("{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCurrency(string code)
        {
            var result = await currencyService.GetCurrencyByCode(code);
            return result.IsSuccess 
                ? Ok(result.Value)
                : NotFound();
        }
        
        /// <summary>
        /// Create new currency.
        /// </summary>
        /// <param name="newCurrency">New currency.</param>
        /// <returns>Created currency code.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Update currency.
        /// </summary>
        /// <param name="id">Currency id.</param>
        /// <param name="updateCurrency">Currency with new parameters.</param>
        /// <returns>Successful update currency.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCurrency(int id, CurrencyRequest updateCurrency)
        {
            var result = await currencyService.UpdateCurrency(id, updateCurrency);
            return result.IsSuccess 
                ? Ok()
                : BadRequest(result.Error);
        }
        
        /// <summary>
        /// Delete currency.
        /// </summary>
        /// <param name="id">Currency id.</param>
        /// <returns>Successful delete currency.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = await currencyService.DeleteCurrency(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error) ;
        }

    }
}
