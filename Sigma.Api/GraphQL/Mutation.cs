using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.Attributes;
using Sigma.Api.Mediator;

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
            [UserId] string userId)
        {
            return await mediator.Send(new CreatePortfolio.Command(input, context, userId));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RemovePortfolio(
            RemovePortfolioInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return await mediator.Send(new RemovePortfolio.Command(input, context));
        }
        
        [Authorize]
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> UpdatePortfolio(
            UpdatePortfolioInput input,
            [ScopedService] FinanceDbContext context, 
            [Service] IMediator mediator)
        {
            return await mediator.Send(new UpdatePortfolio.Command(input, context));
        }
    }
}