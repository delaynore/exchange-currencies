using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchange.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class CurrencyIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Currencies_Code",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "ExchangeRates");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Currencies_Code",
                table: "Currencies");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "ExchangeRates",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Currencies_Code",
                table: "Currencies",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Currencies_CurrencyId",
                table: "ExchangeRates",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id");
        }
    }
}
