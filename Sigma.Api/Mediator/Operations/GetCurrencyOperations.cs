using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Operations
{
    public static class GetCurrencyOperations
    {
        public record Query(string UserId, Guid PortfolioId, IValidationService ValidationService, FinanceDbContext Context) 
            : IRequest<DefaultPayload<IQueryable<CurrencyOperation>>>;
        
        public class Handler : IRequestHandler<Query, DefaultPayload<IQueryable<CurrencyOperation>>>
        {
            public async Task<DefaultPayload<IQueryable<CurrencyOperation>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (userId, portfolioId, validationService, context) = request;

                var error = validationService
                    .PortfolioBelongsUser(portfolioId, userId)
                    .Errors
                    .FirstOrDefault();

                if (error != null)
                {
                    return new DefaultPayload<IQueryable<CurrencyOperation>>(false, error.Message);
                }

                var operations = context.CurrencyOperations
                    .Where(o => o.PortfolioId == portfolioId);

                return new DefaultPayload<IQueryable<CurrencyOperation>>(true, Result: operations);
            }
        }
    }
}