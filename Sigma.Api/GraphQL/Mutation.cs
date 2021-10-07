using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator.Portfolio;
using Sigma.Api.Validations.Interfaces;

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
        public async Task<DefaultPayload> BuyAsset(
            BuyAssetInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return new DefaultPayload(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> SellAsset(
            SellAssetInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return new DefaultPayload(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RefillBalance(
            RefillBalanceInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return new DefaultPayload(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> WithdrawalBalance(
            WithdrawalBalanceInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return new DefaultPayload(true);
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> AddPaymentInPortfolio(
            PaymentInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return new DefaultPayload(true);
        }
        
        
    }
}