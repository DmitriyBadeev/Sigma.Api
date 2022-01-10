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
    public class GetHerfindahlHirschmanIndex
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, 
            IAggregatePortfolioService AggregatePortfolioService, IAnalyticService AnalyticService, string UserId) : IRequest<DefaultPayload<HerfindahlHirschmanIndex>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<HerfindahlHirschmanIndex>>
        {
            public async Task<DefaultPayload<HerfindahlHirschmanIndex>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, aggregatePortfolioService, analyticService, userId) = request;

                var ids = portfolioIds.ToArray();
                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<HerfindahlHirschmanIndex>(false, error.Message);
                }

                var portfolio = await aggregatePortfolioService.Aggregate(ids);
                
                if (portfolio is null)
                {
                    return new DefaultPayload<HerfindahlHirschmanIndex>(true, "Индекс Херфиндаля-Хиршмана посчитан");
                }
                
                var index = analyticService.GetHerfindahlHirschmanIndex(portfolio);

                return new DefaultPayload<HerfindahlHirschmanIndex>(true, "Индекс Херфиндаля-Хиршмана посчитан", index);
            }
        }
    }
}