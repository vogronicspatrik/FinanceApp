using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Data.Migrations
{
    public partial class UpdateWalletTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpDate",
                table: "cards",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "CardholderName",
                table: "cards",
                newName: "Owner");

            migrationBuilder.RenameColumn(
                name: "CardNumber",
                table: "cards",
                newName: "Currency");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "cards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Balance",
                table: "cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "cards");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "cards");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "cards",
                newName: "ExpDate");

            migrationBuilder.RenameColumn(
                name: "Owner",
                table: "cards",
                newName: "CardholderName");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "cards",
                newName: "CardNumber");
        }
    }
}
