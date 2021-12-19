using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class FixDailyPortfolioReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentProfit",
                table: "DailyPortfolioReports",
                newName: "RubBalance");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "DailyPortfolioReports",
                newName: "PaperProfitPercent");

            migrationBuilder.AddColumn<decimal>(
                name: "DividendProfit",
                table: "DailyPortfolioReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DividendProfitPercent",
                table: "DailyPortfolioReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DollarBalance",
                table: "DailyPortfolioReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "EuroBalance",
                table: "DailyPortfolioReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "InvestedSum",
                table: "DailyPortfolioReports",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DividendProfit",
                table: "DailyPortfolioReports");

            migrationBuilder.DropColumn(
                name: "DividendProfitPercent",
                table: "DailyPortfolioReports");

            migrationBuilder.DropColumn(
                name: "DollarBalance",
                table: "DailyPortfolioReports");

            migrationBuilder.DropColumn(
                name: "EuroBalance",
                table: "DailyPortfolioReports");

            migrationBuilder.DropColumn(
                name: "InvestedSum",
                table: "DailyPortfolioReports");

            migrationBuilder.RenameColumn(
                name: "RubBalance",
                table: "DailyPortfolioReports",
                newName: "PaymentProfit");

            migrationBuilder.RenameColumn(
                name: "PaperProfitPercent",
                table: "DailyPortfolioReports",
                newName: "Balance");
        }
    }
}
