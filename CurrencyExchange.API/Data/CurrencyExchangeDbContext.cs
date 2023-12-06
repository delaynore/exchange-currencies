using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CurrencyExchange.API.Data
{
    public class CurrencyExchangeDbContext(DbContextOptions<CurrencyExchangeDbContext> opts) : DbContext(opts)
    {
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<ExchangeRate> ExchangeRates => Set<ExchangeRate>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
