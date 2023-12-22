using CurrencyExchange.API.Data;
using CurrencyExchange.API.Mapping;
using CurrencyExchange.API.Middleware;
using CurrencyExchange.API.Repositories;
using CurrencyExchange.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<CurrencyExchangeDbContext>(opts =>
{
    opts.UseSqlite(builder.Configuration.GetConnectionString("CurrencyExchangeDb"));
});
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();
SeedData.EnsurePopulated(app);
app.Run();
