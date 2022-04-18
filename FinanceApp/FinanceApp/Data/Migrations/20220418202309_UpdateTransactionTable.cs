using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Data.Migrations
{
    public partial class UpdateTransactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "transactions",
                newName: "Notice");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "transactions",
                newName: "WalletId");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "transactions",
                newName: "Type");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "transactions");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "transactions",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "transactions",
                newName: "CardId");

            migrationBuilder.RenameColumn(
                name: "Notice",
                table: "transactions",
                newName: "Description");
        }
    }
}
