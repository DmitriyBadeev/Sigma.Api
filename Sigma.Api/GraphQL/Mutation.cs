using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using Sigma.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator.ExcelReports;
using Sigma.Api.Mediator.Operations;
using Sigma.Api.Mediator.Portfolios;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Services.Interfaces;

namespace Sigma.Api.GraphQL
{
    /// <summary>
    /// Represents the mutations available.
    /// </summary>
    public class Mutation
    {
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> AddPortfolio(
            AddPortfolioInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [UserId] string userId)
        {
            return await mediator.Send(new CreatePortfolio.Command(input, context, validationService, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RemovePortfolio(
            RemovePortfolioInput input,
            [ScopedService] FinanceDbContext context,
            [Service] IValidationService validationService,
            [Service] IMediator mediator)
        {
            return await mediator.Send(new RemovePortfolio.Command(input, validationService, context));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> UpdatePortfolio(
            UpdatePortfolioInput input,
            [ScopedService] FinanceDbContext context,
            [Service] IValidationService validationService,
            [Service] IMediator mediator)
        {
            return await mediator.Send(new UpdatePortfolio.Command(input, validationService, context));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> CreateAssetOperation(
            AssetOperationInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(
                new CreateAssetOperation.Command(input, context, validationService, userId, synchronizationService));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RemoveAssetOperation(
            Guid assetOperationId,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(
                new RemoveAssetOperation.Command(assetOperationId, context, validationService, userId, synchronizationService));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> CreateCurrencyOperation(
            CurrencyOperationInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(
                new CreateCurrencyOperation.Command(input, context, validationService, userId, synchronizationService));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RemoveCurrencyOperation(
            Guid currencyOperationId,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(
                new RemoveCurrencyOperation.Command(currencyOperationId, context, validationService, userId, synchronizationService));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> CreateAssetOperations(
            List<AssetOperationInput> assetOperations,
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(new CreateAssetOperations.Command(assetOperations, context, validationService, userId, synchronizationService));
        }

        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> CreateCurrencyOperations(
            List<CurrencyOperationInput> currencyOperations,
            [ScopedService] FinanceDbContext context,
            [Service] IMediator mediator,
            [Service] IValidationService validationService,
            [Service] ISynchronizationService synchronizationService,
            [UserId] string userId)
        {
            return await mediator.Send(new CreateCurrencyOperations.Command(currencyOperations, context, validationService, userId, synchronizationService));
        }
    }
}