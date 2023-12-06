using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.API.Data.Configurations
{
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.HasOne(x=>x.TargetCurrency)
                .WithMany(c=>c.ExchangeRates)
                .HasForeignKey(x=>x.TargetCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BaseCurrency)
                .WithMany(c => c.ExchangeRates)
                .HasForeignKey(x => x.BaseCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(x => new { x.TargetCurrencyId, x.BaseCurrencyId });
        }
    }
}
