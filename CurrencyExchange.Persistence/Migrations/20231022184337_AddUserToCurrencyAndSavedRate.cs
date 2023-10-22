using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CurrencyExchange.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToCurrencyAndSavedRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SavedRates",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SavedRates_CreatedById",
                table: "SavedRates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_CreatedById",
                table: "Currencies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Currencies_AspNetUsers_CreatedById",
                table: "Currencies",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SavedRates_AspNetUsers_CreatedById",
                table: "SavedRates",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currencies_AspNetUsers_CreatedById",
                table: "Currencies");

            migrationBuilder.DropForeignKey(
                name: "FK_SavedRates_AspNetUsers_CreatedById",
                table: "SavedRates");

            migrationBuilder.DropIndex(
                name: "IX_SavedRates_CreatedById",
                table: "SavedRates");

            migrationBuilder.DropIndex(
                name: "IX_Currencies_CreatedById",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SavedRates");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Currencies");
        }
    }
}
