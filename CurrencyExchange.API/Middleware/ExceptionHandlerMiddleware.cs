using System.Net;

namespace CurrencyExchange.API.Middleware;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            await context.Response.WriteAsJsonAsync(new
            {
                message = "Internal server error",
                details = e.Message,
            });
        }
    }
}