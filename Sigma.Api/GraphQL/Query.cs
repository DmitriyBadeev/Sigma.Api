﻿using System.Linq;
using HotChocolate;
using HotChocolate.Data;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Service;

namespace Sigma.Api.GraphQL
{
    [GraphQLDescription("Represents the queries available.")]
    public class Query
    {
        [UseDbContext(typeof(FinanceDbContext))]
        [UseProjection]
        public IQueryable<Portfolio> GetPortfolios([ScopedService] FinanceDbContext context)
        {
            return context.Portfolios;
        }
        
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<PortfolioType> GetPortfolioTypes([ScopedService] FinanceDbContext context)
        {
            return context.PortfolioTypes;
        }
        
        [UseDbContext(typeof(FinanceDbContext))]
        [UseProjection]
        public IQueryable<AssetOperation> GetAssetOperations([ScopedService] FinanceDbContext context)
        {
            return context.AssetOperations;
        }

        [UseDbContext(typeof(FinanceDbContext))]
        [UseProjection]
        public DefaultPayload Refresh([ScopedService] FinanceDbContext context, [Service] IMoexService moexService)
        {
            moexService.RefreshBoards();

            return new DefaultPayload(true, ""){};
        }
    }
}