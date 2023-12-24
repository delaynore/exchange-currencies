using CurrencyExchange.API.Response;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
public class ApiBaseController : ControllerBase
{
    protected IActionResult BadRequest(Error error) => BadRequest(new { error });
}