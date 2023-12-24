using CurrencyExchange.API.Response;
using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

/// <summary>
/// Exchanging currencies
/// </summary>
/// <param name="exchangeService"></param>
public class ExchangeController(IExchangeService exchangeService) : ApiBaseController
{
    /// <summary>
    /// Exchange one currency to another.
    /// </summary>
    /// <param name="from">Source currency code.</param>
    /// <param name="to">Target currency code.</param>
    /// <param name="amount">The amount of money exchanged.</param>
    /// <returns>Exchanged amount of money</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Error), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ExchangeCurrency(string from, string to, decimal amount)
    {
        var result = await exchangeService.Exchange(from, to, amount);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error);
    }
}