using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator.ExcelReports;
using Sigma.Api.Mediator.Operations;
using Sigma.Api.Mediator.Portfolios;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

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
            return context.Portfolios.Where(p => p.UserId == userId);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<PortfolioType> GetPortfolioTypes([ScopedService] FinanceDbContext context)
        {
            return context.PortfolioTypes;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<Currency> GetCurrencies([ScopedService] FinanceDbContext context)
        {
            return context.Currencies;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<Stock> GetStocks([ScopedService] FinanceDbContext context)
        {
            return context.Stocks.OrderByDescending(s => s.Capitalization);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<Fond> GetFonds([ScopedService] FinanceDbContext context)
        {
            return context.Fonds;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public IQueryable<Bond> GetBonds([ScopedService] FinanceDbContext context)
        {
            return context.Bonds;
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<AssetOperation>>> GetAssetOperations(
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [UserId] string userId, 
            Guid portfolioId)
        {
            return await mediator.Send(new GetAssetOperations.Query(userId, portfolioId, validationService, context));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<CurrencyOperation>>> GetCurrencyOperations(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [Service] IValidationService validationService,
            [UserId] string userId, 
            Guid portfolioId)
        {
            return await mediator.Send(new GetCurrencyOperations.Query(userId, portfolioId, validationService, context));
        }
        
        [Authorize]
        public string[] GetCurrencyOperationTypes()
        {
            return Enum.GetNames<OperationType>();
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<Portfolio>> AggregatePortfolios(
            [Service] IAggregatePortfolioService aggregatePortfolioService,
            [Service] IValidationService validationService,
            [Service] IMediator mediator,
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return await mediator.Send(new GetAggregatePortfolio.Query(validationService, portfolioIds, aggregatePortfolioService, userId));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<List<StockCandle>> StockCandles(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            string ticket, DateTime from, CandleInterval interval)
        {
            return new List<StockCandle>();
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<List<CostGraphData>> AggregatePortfolioCostGraph(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new List<CostGraphData>();
        }

        [Authorize]
        public string SecretData()
        {
            return "Secret";
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<List<AssetOperation>> ParseAssetReport(
            IFile report,
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [UserId] string userId)
        {
            return await mediator.Send(new ParseOperationReport<AssetOperation>.Command(report, context, validationService, userId));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<List<CurrencyOperation>> ParseCurrencyReport(
            IFile report,
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [UserId] string userId)
        {
            return await mediator.Send(new ParseOperationReport<CurrencyOperation>.Command(report, context, validationService, userId));
        }
    }
}