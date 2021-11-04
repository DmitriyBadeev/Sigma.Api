using System.Linq;
using Sigma.Infrastructure;

namespace Sigma.Services.Services
{
    public record PortfolioParameters(
        decimal Cost = 0,
        decimal InvestedSum = 0,
        decimal Profit = 0,
        decimal ProfitPercent = 0,
        decimal DividendProfit = 0,
        decimal DividendProfitPercent = 0,
        decimal RubBalance = 0,
        decimal DollarBalance = 0,
        decimal EuroBalance = 0);
    
    public class SynchronizationService
    {
        private readonly FinanceDbContext _context;
        private readonly CurrencyOperationHandler _currencyOperationHandler;

        public SynchronizationService(FinanceDbContext context, CurrencyOperationHandler currencyOperationHandler)
        {
            _context = context;
            _currencyOperationHandler = currencyOperationHandler;
        }

        public void Sync()
        {
            var currencyOperations = _context.CurrencyOperations.ToList();
            var portfolioParameters = new PortfolioParameters();
            foreach (var currencyOperation in currencyOperations)
            {
                portfolioParameters = _currencyOperationHandler.Handle(portfolioParameters, currencyOperation);
            }
            
        }
    }
}