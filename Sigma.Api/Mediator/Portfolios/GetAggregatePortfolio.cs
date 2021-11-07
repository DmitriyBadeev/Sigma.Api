using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Services.Interfaces;

namespace Sigma.Api.Mediator.Portfolios
{
    public class GetAggregatePortfolio
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, 
            IAggregatePortfolioService AggregatePortfolioService, string UserId) : IRequest<DefaultPayload<Core.Entities.Portfolio>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<Core.Entities.Portfolio>>
        {
            public async Task<DefaultPayload<Core.Entities.Portfolio>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, aggregatePortfolioService, userId) = request;

                var ids = portfolioIds.ToArray();
                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<Core.Entities.Portfolio>(false, error.Message);
                }

                var portfolio = await aggregatePortfolioService.Aggregate(ids);

                return new DefaultPayload<Core.Entities.Portfolio>(true, "Агрегация прошла успешно", portfolio);
            }
        }
    }
}