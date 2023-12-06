using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Data
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            var context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<CurrencyExchangeDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            if (!context.Currencies.Any() && !context.ExchangeRates.Any())
            {
                var currencies = new Currency[]
                {
                    new() { Code = "RUB", FullName = "Russian Ruble", Sign = "₽" },
                    new() { Code = "USD", FullName = "US Dollar", Sign = "$"},
                    new() { Code = "EUR", FullName = "Euro", Sign = "€"}
                };
                var exchangeRates = new ExchangeRate[]
                {
                    new() { BaseCurrency = currencies[0], TargetCurrency = currencies[1], Rate = 0.011m },
                    new() { BaseCurrency = currencies[1], TargetCurrency = currencies[2], Rate = 0.93m }
                };
                context.Currencies.AddRange(currencies);
                context.ExchangeRates.AddRange(exchangeRates);
                context.SaveChanges();
            }
            
        }
    }
}
