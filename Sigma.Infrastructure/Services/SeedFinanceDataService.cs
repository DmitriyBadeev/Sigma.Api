using System.Linq;
using Sigma.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sigma.Core.Interfaces;

namespace Sigma.Infrastructure.Services
{
    public static class SeedFinanceData
    {
        public static string SBER_TYPE = "СберБанк Инвестор";
        public static string TINKOFF_TYPE = "Тинькофф Инвестиции";

        public static string RUB_TICKET = "RUB";
        public static string EURO_TICKET = "EUR";
        public static string DOLLAR_TICKET = "USD";
    }

    public class SeedFinanceDataService : ISeedDataService
    {
        private readonly ILogger<SeedFinanceDataService> _logger;
        private readonly FinanceDbContext _context;

        public SeedFinanceDataService(ILogger<SeedFinanceDataService> logger, 
            IDbContextFactory<FinanceDbContext> contextFactory)
        {
            _logger = logger;
            _context = contextFactory.CreateDbContext();
        }

        public void Initialise()
        {
            _logger.LogInformation("Seeding database");

            if (_context.Database.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Migrating database");
                _context.Database.Migrate();
                _logger.LogInformation("Database has migrated successfully");
            }
       
            AddPortfolioTypes();
            AddCurrencies();
            
            _logger.LogInformation("Database has seeded successfully");
        }

        private void AddPortfolioTypes()
        {
            if (!_context.PortfolioTypes.Any())
            {
                _logger.LogInformation("Adding portfolio types");

                var sber = new PortfolioType()
                {
                    Name = SeedFinanceData.SBER_TYPE,
                    IconUrl = "https://storage.badeev.info/icons/sber.svg"
                };

                var tinkoff = new PortfolioType()
                {
                    Name = SeedFinanceData.TINKOFF_TYPE,
                    IconUrl = "https://storage.badeev.info/icons/tinkoff.svg"
                };
                
                _context.PortfolioTypes.AddRange(sber, tinkoff);
                _context.SaveChanges();
                
                _logger.LogInformation("Added portfolio types successfully");
            }
        }

        private void AddCurrencies()
        {
            if (!_context.Currencies.Any())
            {
                _logger.LogInformation("Adding currencies");

                var rub = new Currency()
                {
                    Name = "Рубль",
                    Ticket = SeedFinanceData.RUB_TICKET,
                    Sign = "₽",
                    DollarRate = (decimal) 72.73,
                    EuroRate = (decimal) 84.29,
                    RubRate = 1,
                };

                var dollar = new Currency()
                {
                    Name = "Доллар",
                    Ticket = SeedFinanceData.DOLLAR_TICKET,
                    Sign = "$",
                    DollarRate = 1,
                    EuroRate = (decimal) 0.86,
                    RubRate = (decimal) 0.014,
                };

                var euro = new Currency()
                {
                    Name = "Евро",
                    Ticket = SeedFinanceData.EURO_TICKET,
                    Sign = "€",
                    DollarRate = (decimal) 1.16,
                    EuroRate = 1,
                    RubRate = (decimal) 0.012
                };
                
                _context.Currencies.AddRange(rub, dollar, euro);
                _context.SaveChanges();
                
                _logger.LogInformation("Added currencies");
            }
        }
    }
}