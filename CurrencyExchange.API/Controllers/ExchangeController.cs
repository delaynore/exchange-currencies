using CurrencyExchange.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

public class ExchangeController(IExchangeService exchangeService) : ApiBaseController
{
    [HttpGet]
    public IActionResult ExchangeCurrency(string from, string to, decimal amount)
    {
        var result = exchangeService.Exchange(from, to, amount);
        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Error) ;
    }
}