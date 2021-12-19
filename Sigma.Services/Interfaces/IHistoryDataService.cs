using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sigma.Integrations.Moex.Models.Candles;
using Sigma.Services.Models;

namespace Sigma.Services.Interfaces
{
    public interface IHistoryDataService
    {
        Task MakePortfoliosRecord();

        Task<List<CostGraphData>> GetPortfoliosCostGraphData(IEnumerable<Guid> portfolioIds);

        Task<List<Candle>> StockCandles(string ticket, DateTime from, CandleInterval interval);
    }
}