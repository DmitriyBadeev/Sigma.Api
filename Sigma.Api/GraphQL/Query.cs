using System.Linq;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Sigma.Api.Attributes;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.GraphQL
{
    /// <summary>
    /// Represents the queries available.
    /// </summary>
    public class Query
    {
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        [UseProjection]
        public IQueryable<Portfolio> GetPortfolios([ScopedService] FinanceDbContext context, [UserId] string userId)
        {
            return context.Portfolios;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<PortfolioType> GetPortfolioTypes([ScopedService] FinanceDbContext context, [UserId] string userId)
        {
            return context.PortfolioTypes;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        [UseProjection]
        public IQueryable<AssetOperation> GetAssetOperations([ScopedService] FinanceDbContext context, [UserId] string userId)
        {
            return context.AssetOperations;
        }
    }
}