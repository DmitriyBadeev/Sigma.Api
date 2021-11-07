using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services.SynchronizationService
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
        decimal EuroBalance = 0,
        List<PortfolioStock> Stocks = null,
        List<PortfolioFond> Fonds = null,
        List<PortfolioBond> Bonds = null);
    
    public class SynchronizationService : ISynchronizationService
    {
        private readonly FinanceDbContext _context;
        private readonly IMarketDataProvider _marketDataProvider;
        public SynchronizationService(FinanceDbContext context, IMarketDataProvider marketDataProvider)
        {
            _context = context;
            _marketDataProvider = marketDataProvider;
        }

        public async Task SyncPortfolios()
        {
            var portfolios = _context.Portfolios
                .Include(p => p.AssetOperations)
                .ThenInclude(o => o.Currency)
                .Include(p => p.CurrencyOperations)
                .ThenInclude(o => o.Currency)
                .AsSingleQuery()
                .ToList();

            foreach (var portfolio in portfolios)
            {
                var newPortfolioParameters = GetNewPortfolioParameters(portfolio);
                await UpdatePortfolioByParameters(portfolio, newPortfolioParameters);
            }
        }

        public async Task SyncPortfolio(Guid portfolioId)
        {
            var portfolio = _context.Portfolios
                .Include(p => p.AssetOperations)
                .ThenInclude(o => o.Currency)
                .Include(p => p.CurrencyOperations)
                .ThenInclude(o => o.Currency)
                .AsSingleQuery()
                .FirstOrDefault(p => p.Id == portfolioId);

            if (portfolio == null)
            {
                throw new ArgumentException("Портфель не найден");
            }
            
            var newPortfolioParameters = GetNewPortfolioParameters(portfolio);
            await UpdatePortfolioByParameters(portfolio, newPortfolioParameters);
        }

        private PortfolioParameters GetNewPortfolioParameters(Portfolio portfolio)
        {
            var initParameters = new PortfolioParameters();
            
            var currencyOperationHandler = new CurrencyOperationHandler();
            var assetOperationHandler = new AssetOperationHandler(_marketDataProvider);
            
            var currencyOperations = portfolio.CurrencyOperations;
            var assetOperations = portfolio.AssetOperations;
            
            var parametersWithCurrencyOperations = currencyOperationHandler.Handle(initParameters, currencyOperations);
            return assetOperationHandler.Handle(parametersWithCurrencyOperations, assetOperations);
        }

        private async Task UpdatePortfolioByParameters(Portfolio portfolio, PortfolioParameters portfolioParameters)
        {
            portfolio.Cost = portfolioParameters.Cost;
            portfolio.InvestedSum = portfolioParameters.InvestedSum;
            portfolio.PaperProfit = portfolioParameters.Profit;
            portfolio.PaperProfitPercent = portfolioParameters.ProfitPercent;
            portfolio.DividendProfit = portfolioParameters.DividendProfit;
            portfolio.DividendProfitPercent = portfolioParameters.DividendProfitPercent;
            portfolio.DollarBalance = portfolioParameters.DollarBalance;
            portfolio.RubBalance = portfolioParameters.RubBalance;
            portfolio.EuroBalance = portfolioParameters.EuroBalance;

            portfolio.PortfolioStocks = portfolioParameters.Stocks;
            portfolio.PortfolioFonds = portfolioParameters.Fonds;
            portfolio.PortfolioBonds = portfolioParameters.Bonds;

            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }
    }
}