using CurrencyExchange.API.Contracts.ExchangeRate;
using CurrencyExchange.API.Response;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

    /// <summary>
    /// Exchange rates management.
    /// </summary>
    /// <param name="exchangeRateService"></param>
    public class ExchangeRatesController(IExchangeRateService exchangeRateService) : ApiBaseController
    {
        /// <summary>
        /// Get all exchange rates.
        /// </summary>
        /// <returns>Exchange rates.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ExchangeRateResponse>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllExchangeRates()
        {
            var result = await exchangeRateService.GetAll();
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }
        
        /// <summary>
        /// Get exchange rate by codes.
        /// </summary>
        /// <param name="baseCurrency">Code of base currency.</param>
        /// <param name="targetCurrency">Code of target currency.</param>
        /// <returns>Exchange rate.</returns>
        [HttpGet("{baseCurrency}-{targetCurrency}")]
        [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var result = await exchangeRateService.GetByCodes(baseCurrency, targetCurrency);
            return result.IsSuccess 
                ? Ok(result.Value) 
                : NotFound();
        }
        
        /// <summary>
        /// Create new exchange rate.
        /// </summary>
        /// <param name="newExchangeRate">New exchange rate.</param>
        /// <returns>Successful create exchange rate.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateExchangeRate(ExchangeRateRequest newExchangeRate)
        {
            var result = await exchangeRateService.Create(newExchangeRate);
            return result.IsSuccess 
                ? CreatedAtAction(
                    nameof(GetExchangeRate), 
                    new
                    {
                        baseCurrency = result.Value.BaseCurrency.Code, 
                        targetCurrency = result.Value.TargetCurrency.Code
                    },
                    result.Value) 
                : BadRequest(result.Error);
        }
        
        /// <summary>
        /// Update exchange rate.
        /// </summary>
        /// <param name="id">Exchange rate id.</param>
        /// <param name="exchangeRateRequest">Exchange rate with new parameters.</param>
        /// <returns>Successful update exchange rate.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateExchangeRate(int id, ExchangeRateRequest exchangeRateRequest)
        {
            var result = await exchangeRateService.Update(id, exchangeRateRequest);
            return result.IsSuccess 
                ? Ok() 
                : BadRequest(result.Error);
        }
        
        /// <summary>
        /// Delete exchange rate.
        /// </summary>
        /// <param name="id">Exchange rate id.</param>
        /// <returns>Successful delete exchange rate.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteExchangeRate(int id)
        {
            var result = await exchangeRateService.Delete(id);
            return result.IsSuccess 
                ? NoContent() 
                : BadRequest(result.Error);
        }
    }

