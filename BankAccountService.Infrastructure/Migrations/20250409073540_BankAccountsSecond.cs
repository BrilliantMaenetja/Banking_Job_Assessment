using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccountService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountsSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountType",
                table: "BankAcounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BankAcounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountType",
                table: "BankAcounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BankAcounts");
        }
    }
}
