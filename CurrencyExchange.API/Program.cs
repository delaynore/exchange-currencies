using CurrencyExchange.API.Data;
using CurrencyExchange.API.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CurrencyExchangeDbContext>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.Run();
