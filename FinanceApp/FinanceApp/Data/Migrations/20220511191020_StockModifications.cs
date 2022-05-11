using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Data.Migrations
{
    public partial class StockModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "stocks",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "CurrentValue",
                table: "stocks",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_stocks_UserId",
                table: "stocks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_stocks_AspNetUsers_UserId",
                table: "stocks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_stocks_AspNetUsers_UserId",
                table: "stocks");

            migrationBuilder.DropIndex(
                name: "IX_stocks_UserId",
                table: "stocks");

            migrationBuilder.DropColumn(
                name: "CurrentValue",
                table: "stocks");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "stocks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
