using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Api.Mediator.Portfolios
{
    public class GetSharpeRatio
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, 
            IAggregatePortfolioService AggregatePortfolioService, IAnalyticService AnalyticService, string UserId) : IRequest<DefaultPayload<SharpeRatio>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<SharpeRatio>>
        {
            public async Task<DefaultPayload<SharpeRatio>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, aggregatePortfolioService, analyticService, userId) = request;

                var ids = portfolioIds.ToArray();
                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<SharpeRatio>(false, error.Message);
                }

                var portfolio = await aggregatePortfolioService.Aggregate(ids);
                var index = analyticService.GetSharpeRatio(portfolio);

                return new DefaultPayload<SharpeRatio>(true, "Индекс Шарпа посчитан", index);
            }
        }
    }
}