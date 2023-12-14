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
                .WithMany()
                .HasForeignKey(x=>x.TargetCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.BaseCurrency)
                .WithMany()
                .HasForeignKey(x => x.BaseCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new { x.TargetCurrencyId, x.BaseCurrencyId }).IsUnique();
        }
    }
}
