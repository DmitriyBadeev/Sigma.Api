using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sigma.Core.Entities;

namespace Sigma.Services.Interfaces
{
    public interface IAggregatePortfolioService
    {
        Task<Portfolio> Aggregate(IEnumerable<Guid> portfolioIds);
    }
}