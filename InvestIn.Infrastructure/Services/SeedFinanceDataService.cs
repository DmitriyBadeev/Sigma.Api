using System.Linq;
using InvestIn.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using InvestIn.Core.Interfaces;

namespace InvestIn.Infrastructure.Services
{
    public static class SeedFinanceData
    {
        public static string SBER_TYPE = "СберБанк Инвестор";
        public static string TINKOFF_TYPE = "Тинькофф Инвестиции";
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
    }
}