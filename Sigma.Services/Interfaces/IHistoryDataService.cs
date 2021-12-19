using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sigma.Services.Models;

namespace Sigma.Services.Interfaces
{
    public interface IHistoryDataService
    {
        Task MakePortfoliosRecord();

        Task<List<CostGraphData>> GetPortfoliosCostGraphData(IEnumerable<Guid> portfolioIds);
    }
}