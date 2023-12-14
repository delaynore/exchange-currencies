using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchange.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRateIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ExchangeRates_TargetCurrencyId_BaseCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_TargetCurrencyId_BaseCurrencyId",
                table: "ExchangeRates",
                columns: new[] { "TargetCurrencyId", "BaseCurrencyId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_TargetCurrencyId_BaseCurrencyId",
                table: "ExchangeRates");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ExchangeRates_TargetCurrencyId_BaseCurrencyId",
                table: "ExchangeRates",
                columns: new[] { "TargetCurrencyId", "BaseCurrencyId" });
        }
    }
}
