using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Helpers;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services
{
    public class AggregatePortfolioService : IAggregatePortfolioService
    {
        private readonly FinanceDbContext _context;

        public AggregatePortfolioService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task<Portfolio> Aggregate(IEnumerable<Guid> portfolioIds)
        {
            var portfolios = await _context.Portfolios
                .Where(p => portfolioIds.Contains(p.Id))
                .Include(p => p.PortfolioStocks)
                .ThenInclude(s => s.Stock)
                .Include(p => p.PortfolioFonds)
                .ThenInclude(f => f.Fond)
                .Include(p => p.PortfolioBonds)
                .ThenInclude(b => b.Bond)
                .AsSingleQuery()
                .ToListAsync();

            if (portfolios.Count == 0)
            {
                return null;
            }

            var aggregatedPortfolio = new Portfolio
            {
                PortfolioStocks = new List<PortfolioStock>(),
                PortfolioFonds = new List<PortfolioFond>(),
                PortfolioBonds = new List<PortfolioBond>()
            };
            
            foreach (var portfolio in portfolios)
            {
                aggregatedPortfolio.Cost += portfolio.Cost;
                aggregatedPortfolio.PaperProfit += portfolio.PaperProfit;
                aggregatedPortfolio.InvestedSum += portfolio.InvestedSum;
                aggregatedPortfolio.DividendProfit += portfolio.DividendProfit;
                aggregatedPortfolio.RubBalance += portfolio.RubBalance;
                aggregatedPortfolio.DollarBalance += portfolio.DollarBalance;
                aggregatedPortfolio.EuroBalance += portfolio.EuroBalance;

                aggregatedPortfolio.DividendProfitPercent = 
                    ArithmeticHelper.SafeDivFunc(aggregatedPortfolio.DividendProfit, aggregatedPortfolio.InvestedSum);
                aggregatedPortfolio.PaperProfitPercent =
                    ArithmeticHelper.SafeDivFunc(aggregatedPortfolio.PaperProfit, aggregatedPortfolio.InvestedSum);
                
                aggregatedPortfolio.PortfolioStocks.AddRange(portfolio.PortfolioStocks);
                aggregatedPortfolio.PortfolioFonds.AddRange(portfolio.PortfolioFonds);
                aggregatedPortfolio.PortfolioBonds.AddRange(portfolio.PortfolioBonds);
            }

            return aggregatedPortfolio;
        }
    }
}