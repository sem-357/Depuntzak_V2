using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Depuntzak_V2.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlaceOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CustomerId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId1",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId1",
                table: "Transaction",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CustomerId1",
                table: "Transaction",
                column: "CustomerId1",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Customer_CustomerId1",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CustomerId1",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Customer_CustomerId",
                table: "Transaction",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }
    }
}
