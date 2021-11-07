using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class AddPortfolioParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "PortfolioStocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfit",
                table: "PortfolioStocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfitPercent",
                table: "PortfolioStocks",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DividendProfit",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DividendProfitPercent",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DollarBalance",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EuroBalance",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvestedSum",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfit",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfitPercent",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RubBalance",
                table: "Portfolios",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "PortfolioFonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfit",
                table: "PortfolioFonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfitPercent",
                table: "PortfolioFonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "PortfolioBonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfit",
                table: "PortfolioBonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PaperProfitPercent",
                table: "PortfolioBonds",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "AssetOperations",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "PortfolioStocks");

            migrationBuilder.DropColumn(
                name: "PaperProfit",
                table: "PortfolioStocks");

            migrationBuilder.DropColumn(
                name: "PaperProfitPercent",
                table: "PortfolioStocks");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "DividendProfit",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "DividendProfitPercent",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "DollarBalance",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "EuroBalance",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "InvestedSum",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PaperProfit",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "PaperProfitPercent",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "RubBalance",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "PortfolioFonds");

            migrationBuilder.DropColumn(
                name: "PaperProfit",
                table: "PortfolioFonds");

            migrationBuilder.DropColumn(
                name: "PaperProfitPercent",
                table: "PortfolioFonds");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "PortfolioBonds");

            migrationBuilder.DropColumn(
                name: "PaperProfit",
                table: "PortfolioBonds");

            migrationBuilder.DropColumn(
                name: "PaperProfitPercent",
                table: "PortfolioBonds");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "AssetOperations");
        }
    }
}
