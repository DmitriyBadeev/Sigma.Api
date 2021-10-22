using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Operations
{
    public static class GetCurrencyOperations
    {
        public record Query(string UserId, Guid PortfolioId, IValidationService ValidationService, FinanceDbContext Context) 
            : IRequest<DefaultPayload<List<CurrencyOperation>>>;
        
        public class Handler : IRequestHandler<Query, DefaultPayload<List<CurrencyOperation>>>
        {
            public async Task<DefaultPayload<List<CurrencyOperation>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (userId, portfolioId, validationService, context) = request;

                var error = validationService
                    .PortfolioBelongsUser(portfolioId, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<List<CurrencyOperation>>(false, error.Message);
                }

                var operations = context.CurrencyOperations
                    .Include(o => o.Currency)
                    .Include(o => o.Portfolio)
                    .ThenInclude(o => o.PortfolioType)
                    .Where(o => o.PortfolioId == portfolioId)
                    .ToList();

                return new DefaultPayload<List<CurrencyOperation>>(true, Result: operations);
            }
        }
    }
}