using CurrencyExchange.API.Data;
using CurrencyExchange.API.Middleware;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CurrencyExchangeDbContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("CurrencyExchangeDb"));
});
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();
SeedData.EnsurePopulated(app);
app.Run();
