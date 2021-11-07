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
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
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
        public async Task<DefaultPayload<decimal>> AggregateBalance(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<decimal>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<decimal>> AggregateInvestSum(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<decimal>(true);
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<PaymentData>>> AggregateFuturePayments(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<List<PaymentData>>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<ValuePercent>> AggregatePortfolioPaymentProfit(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<ValuePercent>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<ValuePercent>> AggregatePortfolioPaperProfit(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<ValuePercent>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<decimal>> AggregatePortfolioCost(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<decimal>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<CostWithInvestSum>> AggregatePortfolioCostWithInvestSum(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<CostWithInvestSum>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<StockReport>>> AggregateStocks(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<List<StockReport>>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<FondReport>>> AggregateFonds(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<List<FondReport>>(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<BondReport>>> AggregateBonds(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return new DefaultPayload<List<BondReport>>(true);
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
        public List<TimeValue> PortfolioCostGraph(
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator, 
            [UserId] string userId, 
            int portfolioId)
        {
            return new List<TimeValue>();
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