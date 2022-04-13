using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Data.Migrations
{
    public partial class TransactionListRemovedFromModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions");

            migrationBuilder.DropIndex(
                name: "IX_transactions_CardId",
                table: "transactions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_transactions_CardId",
                table: "transactions",
                column: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_transactions_cards_CardId",
                table: "transactions",
                column: "CardId",
                principalTable: "cards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
