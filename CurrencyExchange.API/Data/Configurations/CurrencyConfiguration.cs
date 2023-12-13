using CurrencyExchange.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchange.API.Data.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasIndex(x => x.Code).IsUnique();
            builder.Property(x=>x.FullName).HasMaxLength(100);
            builder.Property(x => x.Code).HasColumnType("varchar(3)");
            builder.Property(x => x.Sign).HasMaxLength(3);

        }
    }
}
