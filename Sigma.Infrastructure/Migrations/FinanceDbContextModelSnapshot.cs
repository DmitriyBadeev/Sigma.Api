﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sigma.Infrastructure;

namespace Sigma.Infrastructure.Migrations
{
    [DbContext(typeof(FinanceDbContext))]
    partial class FinanceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Sigma.Core.Entities.AssetOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("AssetAction")
                        .HasColumnType("integer");

                    b.Property<int>("AssetType")
                        .HasColumnType("integer");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("AssetOperations");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Bond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AmortizationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Coupon")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("LatName")
                        .HasColumnType("text");

                    b.Property<string>("MarketFullName")
                        .HasColumnType("text");

                    b.Property<decimal>("Nominal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Percent")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PercentChange")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Ticket")
                        .IsUnique();

                    b.ToTable("Bonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BondId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CouponDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ValuePercent")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BondId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Currency", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("DollarRate")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EuroRate")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("RubRate")
                        .HasColumnType("numeric");

                    b.Property<string>("Sign")
                        .HasColumnType("text");

                    b.Property<string>("Ticket")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("Sigma.Core.Entities.CurrencyOperation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("OperationType")
                        .HasColumnType("integer");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.Property<string>("Ticket")
                        .HasColumnType("text");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("CurrencyOperations");
                });

            modelBuilder.Entity("Sigma.Core.Entities.DailyPortfolioReport", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("DividendProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DividendProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DollarBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EuroBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("InvestedSum")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("RubBalance")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("DailyPortfolioReports");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Dividend", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CurrencyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("RegistryCloseDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("StockId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("StockId");

                    b.ToTable("Dividends");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Fond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("LatName")
                        .HasColumnType("text");

                    b.Property<string>("MarketFullName")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PriceChange")
                        .HasColumnType("numeric");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Ticket")
                        .IsUnique();

                    b.ToTable("Fonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Portfolio", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DividendProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DividendProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<decimal>("DollarBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("EuroBalance")
                        .HasColumnType("numeric");

                    b.Property<decimal>("InvestedSum")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("PaperProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PortfolioTypeId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("RubBalance")
                        .HasColumnType("numeric");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioTypeId");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioBond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("BondId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("BoughtPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BondId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioBonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioFond", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<decimal>("BoughtPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<Guid>("FondId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("PaperProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("FondId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioFonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<decimal>("BoughtPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Cost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfit")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PaperProfitPercent")
                        .HasColumnType("numeric");

                    b.Property<Guid>("PortfolioId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StockId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("StockId");

                    b.ToTable("PortfolioStocks");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("IconUrl")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PortfolioTypes");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Capitalization")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<long>("IssueSize")
                        .HasColumnType("bigint");

                    b.Property<string>("LatName")
                        .HasColumnType("text");

                    b.Property<int>("LotSize")
                        .HasColumnType("integer");

                    b.Property<string>("MarketFullName")
                        .HasColumnType("text");

                    b.Property<decimal>("PrevClosePrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PriceChange")
                        .HasColumnType("numeric");

                    b.Property<string>("Sector")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Ticket")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Ticket")
                        .IsUnique();

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Sigma.Core.Entities.AssetOperation", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Currency", "Currency")
                        .WithMany("AssetOperations")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("AssetOperations")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Coupon", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Bond", "Bond")
                        .WithMany("Coupons")
                        .HasForeignKey("BondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bond");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("Sigma.Core.Entities.CurrencyOperation", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Currency", "Currency")
                        .WithMany("CurrencyOperations")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("CurrencyOperations")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Sigma.Core.Entities.DailyPortfolioReport", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("DailyPortfolioReports")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Dividend", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Currency", "Currency")
                        .WithMany()
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Stock", "Stock")
                        .WithMany("Dividends")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Currency");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Portfolio", b =>
                {
                    b.HasOne("Sigma.Core.Entities.PortfolioType", "PortfolioType")
                        .WithMany("Portfolios")
                        .HasForeignKey("PortfolioTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PortfolioType");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioBond", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Bond", "Bond")
                        .WithMany("PortfolioBonds")
                        .HasForeignKey("BondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("PortfolioBonds")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bond");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioFond", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Fond", "Fond")
                        .WithMany("PortfolioFonds")
                        .HasForeignKey("FondId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("PortfolioFonds")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fond");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioStock", b =>
                {
                    b.HasOne("Sigma.Core.Entities.Portfolio", "Portfolio")
                        .WithMany("PortfolioStocks")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sigma.Core.Entities.Stock", "Stock")
                        .WithMany("PortfolioStocks")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Portfolio");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Bond", b =>
                {
                    b.Navigation("Coupons");

                    b.Navigation("PortfolioBonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Currency", b =>
                {
                    b.Navigation("AssetOperations");

                    b.Navigation("CurrencyOperations");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Fond", b =>
                {
                    b.Navigation("PortfolioFonds");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Portfolio", b =>
                {
                    b.Navigation("AssetOperations");

                    b.Navigation("CurrencyOperations");

                    b.Navigation("DailyPortfolioReports");

                    b.Navigation("PortfolioBonds");

                    b.Navigation("PortfolioFonds");

                    b.Navigation("PortfolioStocks");
                });

            modelBuilder.Entity("Sigma.Core.Entities.PortfolioType", b =>
                {
                    b.Navigation("Portfolios");
                });

            modelBuilder.Entity("Sigma.Core.Entities.Stock", b =>
                {
                    b.Navigation("Dividends");

                    b.Navigation("PortfolioStocks");
                });
#pragma warning restore 612, 618
        }
    }
}
