using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountHolderSecond : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityNo",
                table: "AccountHolders",
                type: "int",
                maxLength: 13,
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityNo",
                table: "AccountHolders");
        }
    }
}
