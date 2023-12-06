using CurrencyExchange.API.Data;
using CurrencyExchange.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<CurrencyExchangeDbContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("CurrencyExchangeDb"));
});
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();


var app = builder.Build();


app.MapControllers();
SeedData.EnsurePopulated(app);
app.Run();
