﻿using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.Mediator;

namespace Sigma.Api.GraphQL
{
    [GraphQLDescription("Represents the mutations available.")]
    public class Mutation
    {
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> AddPortfolio(AddPortfolioInput input,
            [ScopedService] FinanceDbContext context, [Service] IMediator mediator)
        {
            return await mediator.Send(new CreatePortfolio.Command(input, context));
        }
        
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> RemovePortfolio(RemovePortfolioInput input,
            [ScopedService] FinanceDbContext context, [Service] IMediator mediator)
        {
            return await mediator.Send(new RemovePortfolio.Command(input, context));
        }
        
        [UseDbContext(typeof(FinanceDbContext))]
        public async Task<DefaultPayload> UpdatePortfolio(UpdatePortfolioInput input,
            [ScopedService] FinanceDbContext context, [Service] IMediator mediator)
        {
            return await mediator.Send(new UpdatePortfolio.Command(input, context));
        }
    }
}