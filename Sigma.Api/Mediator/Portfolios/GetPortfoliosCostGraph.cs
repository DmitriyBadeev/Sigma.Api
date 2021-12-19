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
    public class GetPortfoliosCostGraph
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, 
            IHistoryDataService HistoryDataService, string UserId) : IRequest<DefaultPayload<List<CostGraphData>>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<List<CostGraphData>>>
        {
            public async Task<DefaultPayload<List<CostGraphData>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, historyDataService, userId) = request;

                var ids = portfolioIds.ToArray();
                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<List<CostGraphData>>(false, error.Message);
                }

                var graphData = await historyDataService.GetPortfoliosCostGraphData(ids);

                return new DefaultPayload<List<CostGraphData>>(true, "Данные для графа получены", graphData);
            }
        }
    }
}