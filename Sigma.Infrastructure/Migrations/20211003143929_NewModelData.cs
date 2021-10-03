using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class NewModelData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "DailyPortfolioReports",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "CurrencyName",
                table: "CurrencyOperations",
                newName: "Ticket");

            migrationBuilder.RenameColumn(
                name: "CurrencyAction",
                table: "CurrencyOperations",
                newName: "OperationType");

            migrationBuilder.AlterColumn<Guid>(
                name: "CurrencyId",
                table: "CurrencyOperations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "CurrencyOperations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CurrencyId",
                table: "AssetOperations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Ticket = table.Column<string>(type: "text", nullable: true),
                    RubRate = table.Column<decimal>(type: "numeric", nullable: false),
                    DollarRate = table.Column<decimal>(type: "numeric", nullable: false),
                    EuroRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_Ticket",
                table: "Stocks",
                column: "Ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fonds_Ticket",
                table: "Fonds",
                column: "Ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyOperations_CurrencyId",
                table: "CurrencyOperations",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonds_Ticket",
                table: "Bonds",
                column: "Ticket",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetOperations_CurrencyId",
                table: "AssetOperations",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetOperations_Currencies_CurrencyId",
                table: "AssetOperations",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyOperations_Currencies_CurrencyId",
                table: "CurrencyOperations",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetOperations_Currencies_CurrencyId",
                table: "AssetOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyOperations_Currencies_CurrencyId",
                table: "CurrencyOperations");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_Ticket",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Fonds_Ticket",
                table: "Fonds");

            migrationBuilder.DropIndex(
                name: "IX_CurrencyOperations_CurrencyId",
                table: "CurrencyOperations");

            migrationBuilder.DropIndex(
                name: "IX_Bonds_Ticket",
                table: "Bonds");

            migrationBuilder.DropIndex(
                name: "IX_AssetOperations_CurrencyId",
                table: "AssetOperations");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "CurrencyOperations");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "AssetOperations");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DailyPortfolioReports",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "Ticket",
                table: "CurrencyOperations",
                newName: "CurrencyName");

            migrationBuilder.RenameColumn(
                name: "OperationType",
                table: "CurrencyOperations",
                newName: "CurrencyAction");

            migrationBuilder.AlterColumn<string>(
                name: "CurrencyId",
                table: "CurrencyOperations",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AllPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PaymentPerOne = table.Column<decimal>(type: "numeric", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PortfolioId",
                table: "Payments",
                column: "PortfolioId");
        }
    }
}
