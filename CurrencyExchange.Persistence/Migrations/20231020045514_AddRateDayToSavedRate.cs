using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchange.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRateDayToSavedRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "RateDay",
                table: "SavedRates",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1970, 1, 1));

            migrationBuilder.AddUniqueConstraint(
                "SavedRates_CurrencyId_RateDay_key",
                "SavedRates",
                new[] {"CurrencyId", "RateDay"});
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint("SavedRates_CurrencyId_RateDay_key", "SavedRates");

            migrationBuilder.DropColumn(
                name: "RateDay",
                table: "SavedRates");
        }
    }
}
