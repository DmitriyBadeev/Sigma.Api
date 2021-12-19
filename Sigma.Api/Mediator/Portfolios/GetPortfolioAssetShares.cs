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
    public class GetPortfolioAssetShares
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, 
            IAggregatePortfolioService AggregatePortfolioService, IAnalyticService AnalyticService, string UserId) : IRequest<DefaultPayload<List<AssetShare>>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<List<AssetShare>>>
        {
            public async Task<DefaultPayload<List<AssetShare>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, aggregatePortfolioService, analyticService, userId) = request;

                var ids = portfolioIds.ToArray();
                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<List<AssetShare>>(false, error.Message);
                }

                var portfolio = await aggregatePortfolioService.Aggregate(ids);
                var shares = analyticService.GetPortfolioAssetShares(portfolio);

                return new DefaultPayload<List<AssetShare>>(true, "Доли посчитаны успешно", shares);
            }
        }
    }
}