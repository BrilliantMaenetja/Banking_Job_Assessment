using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountHolder.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AccountHolderThirds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdentityNo",
                table: "AccountHolders",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 13);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdentityNo",
                table: "AccountHolders",
                type: "int",
                maxLength: 13,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13,
                oldNullable: true);
        }
    }
}
