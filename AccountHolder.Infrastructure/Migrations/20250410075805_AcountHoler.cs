using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AcountHoler : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "AccountHolders");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "AccountHolders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "AccountHolders");

            migrationBuilder.AddColumn<string>(
                name: "IdentityNo",
                table: "AccountHolders",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true);
        }
    }
}
