using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenDonut;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Data.Sorting.Expressions;
using HotChocolate.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator.ExcelReports;
using Sigma.Api.Mediator.Operations;
using Sigma.Api.Mediator.Portfolios;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Models.Candles;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

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
            return context.Portfolios.Where(p => p.UserId == userId).OrderBy(p => p.Name);
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
        public async Task<List<Candle>> StockCandles(
            [Service] IHistoryDataService historyDataService,
            string ticket, DateTime from, CandleInterval interval)
        {
            return await historyDataService.StockCandles(ticket, from, interval);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<CostGraphData>>> GetPortfoliosCostGraph(
            [Service] IMediator mediator,
            [Service] IHistoryDataService historyDataService,
            [Service] IValidationService validationService,
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return await mediator.Send(new GetPortfoliosCostGraph.Query(validationService, portfolioIds, historyDataService, userId));
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
            return await mediator.Send(new ParseAssetReport.Command(report, context, validationService, userId));
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
            return await mediator.Send(new ParseCurrencyReport.Command(report, context, validationService, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<PaymentData>>> GetFuturePayments(
            IEnumerable<Guid> portfolioIds,
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] IPaymentService paymentService,
            [UserId] string userId)
        {
            return await mediator.Send(new GetFuturePayments.Query(validationService, portfolioIds, userId, context, paymentService));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<List<AssetShare>>> GetPortfolioAssetShares(
            [Service] IAggregatePortfolioService aggregatePortfolioService,
            [Service] IValidationService validationService,
            [Service] IMediator mediator,
            [Service] IAnalyticService analyticService,
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return await mediator.Send(new GetPortfolioAssetShares.Query(validationService, portfolioIds, aggregatePortfolioService, analyticService, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<HerfindahlHirschmanIndex>> GetHerfindahlHirschmanIndex(
            [Service] IAggregatePortfolioService aggregatePortfolioService,
            [Service] IValidationService validationService,
            [Service] IMediator mediator,
            [Service] IAnalyticService analyticService,
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return await mediator.Send(new GetHerfindahlHirschmanIndex.Query(validationService, portfolioIds, aggregatePortfolioService, analyticService, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload<SharpeRatio>> GetSharpeRatio(
            [Service] IAggregatePortfolioService aggregatePortfolioService,
            [Service] IValidationService validationService,
            [Service] IMediator mediator,
            [Service] IAnalyticService analyticService,
            [UserId] string userId, 
            IEnumerable<Guid> portfolioIds)
        {
            return await mediator.Send(new GetSharpeRatio.Query(validationService, portfolioIds, aggregatePortfolioService, analyticService, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> SyncPortfolios([Service] ISynchronizationService synchronizationService)
        { 
            await synchronizationService.SyncPortfolios();
            return new DefaultPayload(true, "Синхронизация прошла успешно");
        }
    }
}