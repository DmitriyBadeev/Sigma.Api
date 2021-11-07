using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Portfolio
{
    public static class UpdatePortfolio
    {
        public record Command(UpdatePortfolioInput Input, IValidationService ValidationService, FinanceDbContext Context) 
            : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        { 
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, validationService, context) = request;

                var error = validationService
                    .CheckExist<Core.Entities.Portfolio>(input.PortfolioId)
                    .CheckExist<PortfolioType>(input.TypeId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }
                
                var portfolio = await context.Portfolios.FindAsync(input.PortfolioId);
                var portfolioType = await context.PortfolioTypes.FindAsync(input.TypeId);

                portfolio.Name = input.Name;
                portfolio.PortfolioTypeId = input.TypeId;
                portfolio.PortfolioType = portfolioType;
            
                context.Portfolios.Update(portfolio);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Портфель обновлен");
            }
        }
    }
}