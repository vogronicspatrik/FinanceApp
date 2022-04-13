using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Data.Migrations
{
    public partial class FixedFKinTransTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "cards");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "transactions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions");

            migrationBuilder.AlterColumn<int>(
                name: "CardId",
                table: "transactions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id");
        }
    }
}
