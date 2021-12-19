using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Extensions;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Services.Services
{
    public class HistoryDataService : IHistoryDataService
    {
        private readonly FinanceDbContext _context;

        public HistoryDataService(FinanceDbContext context)
        {
            _context = context;
        }

        public async Task MakePortfoliosRecord()
        {
            var portfolios = await _context.Portfolios.ToListAsync();

            foreach (var portfolio in portfolios)
            {
                var report = new DailyPortfolioReport()
                {
                    Cost = portfolio.Cost,
                    PaperProfit = portfolio.PaperProfit,
                    PaperProfitPercent = portfolio.PaperProfitPercent,
                    InvestedSum = portfolio.InvestedSum,
                    DividendProfit = portfolio.DividendProfit,
                    DividendProfitPercent = portfolio.DividendProfitPercent,
                    RubBalance = portfolio.RubBalance,
                    DollarBalance = portfolio.DollarBalance,
                    EuroBalance = portfolio.EuroBalance,
                    Date = DateTime.Today,
                    Portfolio = portfolio,
                    PortfolioId = portfolio.Id,
                };

                await _context.DailyPortfolioReports.AddAsync(report);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<CostGraphData>> GetPortfoliosCostGraphData(IEnumerable<Guid> portfolioIds)
        {
            var graphData = new List<CostGraphData>();
            
            foreach (var portfolioId in portfolioIds)
            {
                var portfolio = await _context.Portfolios.FindAsync(portfolioId);
                
                var dailyReports = _context.DailyPortfolioReports
                    .Where(r => r.PortfolioId == portfolioId)
                    .ToList();

                var values = dailyReports
                    .Select(r => new TimeValue(r.Date.MillisecondsTimestamp(), r.Cost))
                    .ToList();

                var portfolioGraphData = new CostGraphData(portfolioId, portfolio.Name, values);
                
                graphData.Add(portfolioGraphData);
            }

            return graphData;
        }
    }
}