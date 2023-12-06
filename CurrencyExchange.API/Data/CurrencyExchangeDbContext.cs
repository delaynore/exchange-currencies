using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchange.API.Data
{
    public class CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> opts) : DbContext(opts)
    {
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();

    }
}
