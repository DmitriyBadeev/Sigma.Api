using Sigma.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Sigma.Infrastructure
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<AssetOperation> AssetOperations { get; set; }
        public DbSet<CurrencyOperation> CurrencyOperations { get; set; }
        public DbSet<DailyPortfolioReport> DailyPortfolioReports { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<PortfolioType> PortfolioTypes { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Fond> Fonds { get; set; }
        public DbSet<Bond> Bonds { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioStock> PortfolioStocks { get; set; }
        public DbSet<PortfolioFond> PortfolioFonds { get; set; }
        public DbSet<PortfolioBond> PortfolioBonds { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<Coupon> Coupons { get; set; }

        public FinanceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}