using System;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator.Operations;
using Sigma.Api.Mediator.Portfolio;
using Sigma.Api.Validations.Interfaces;
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


    }
}