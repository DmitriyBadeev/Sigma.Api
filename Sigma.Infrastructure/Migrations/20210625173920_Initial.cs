using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sigma.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bonds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    MarketFullName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    LatName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Percent = table.Column<decimal>(type: "numeric", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentChange = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AmortizationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Nominal = table.Column<decimal>(type: "numeric", nullable: false),
                    Coupon = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fonds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    MarketFullName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    LatName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceChange = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IconUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: false),
                    MarketFullName = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    LatName = table.Column<string>(type: "text", nullable: true),
                    LotSize = table.Column<int>(type: "integer", nullable: false),
                    IssueSize = table.Column<long>(type: "bigint", nullable: false),
                    PrevClosePrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Capitalization = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Sector = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PriceChange = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    PortfolioTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolios_PortfolioTypes_PortfolioTypeId",
                        column: x => x.PortfolioTypeId,
                        principalTable: "PortfolioTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetType = table.Column<int>(type: "integer", nullable: false),
                    AssetAction = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetOperations_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyOperations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrencyName = table.Column<string>(type: "text", nullable: true),
                    CurrencyId = table.Column<string>(type: "text", nullable: true),
                    Total = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CurrencyAction = table.Column<int>(type: "integer", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyOperations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyOperations_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyPortfolioReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric", nullable: false),
                    PaperProfit = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentProfit = table.Column<decimal>(type: "numeric", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPortfolioReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyPortfolioReports_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ticket = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    PaymentPerOne = table.Column<decimal>(type: "numeric", nullable: false),
                    AllPayment = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PortfolioBonds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    BoughtPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    BondId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioBonds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioBonds_Bonds_BondId",
                        column: x => x.BondId,
                        principalTable: "Bonds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioBonds_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioFonds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    BoughtPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    FondId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioFonds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioFonds_Fonds_FondId",
                        column: x => x.FondId,
                        principalTable: "Fonds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioFonds_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortfolioStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    BoughtPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "uuid", nullable: false),
                    StockId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioStocks_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioStocks_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetOperations_PortfolioId",
                table: "AssetOperations",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyOperations_PortfolioId",
                table: "CurrencyOperations",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyPortfolioReports_PortfolioId",
                table: "DailyPortfolioReports",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PortfolioId",
                table: "Payments",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioBonds_BondId",
                table: "PortfolioBonds",
                column: "BondId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioBonds_PortfolioId",
                table: "PortfolioBonds",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioFonds_FondId",
                table: "PortfolioFonds",
                column: "FondId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioFonds_PortfolioId",
                table: "PortfolioFonds",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_PortfolioTypeId",
                table: "Portfolios",
                column: "PortfolioTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioStocks_PortfolioId",
                table: "PortfolioStocks",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioStocks_StockId",
                table: "PortfolioStocks",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetOperations");

            migrationBuilder.DropTable(
                name: "CurrencyOperations");

            migrationBuilder.DropTable(
                name: "DailyPortfolioReports");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "PortfolioBonds");

            migrationBuilder.DropTable(
                name: "PortfolioFonds");

            migrationBuilder.DropTable(
                name: "PortfolioStocks");

            migrationBuilder.DropTable(
                name: "Bonds");

            migrationBuilder.DropTable(
                name: "Fonds");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "PortfolioTypes");
        }
    }
}
