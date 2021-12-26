using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AverageProfit",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Risk",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SharpeRatio",
                table: "Stocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageProfit",
                table: "Fonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Risk",
                table: "Fonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SharpeRatio",
                table: "Fonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AverageProfit",
                table: "Bonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CouponPercent",
                table: "Bonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Risk",
                table: "Bonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SharpeRatio",
                table: "Bonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageProfit",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "Risk",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SharpeRatio",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "AverageProfit",
                table: "Fonds");

            migrationBuilder.DropColumn(
                name: "Risk",
                table: "Fonds");

            migrationBuilder.DropColumn(
                name: "SharpeRatio",
                table: "Fonds");

            migrationBuilder.DropColumn(
                name: "AverageProfit",
                table: "Bonds");

            migrationBuilder.DropColumn(
                name: "CouponPercent",
                table: "Bonds");

            migrationBuilder.DropColumn(
                name: "Risk",
                table: "Bonds");

            migrationBuilder.DropColumn(
                name: "SharpeRatio",
                table: "Bonds");
        }
    }
}
