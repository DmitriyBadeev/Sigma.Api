using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Portfolio
{
    public static class RemovePortfolio
    {
        public record Command(RemovePortfolioInput Input, IValidationService ValidationService, FinanceDbContext Context) : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        { 
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, validationService, context) = request;

                var error = validationService
                    .CheckPortfolioExist(input.PortfolioId)
                    .Errors
                    .FirstOrDefault();
                
                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }

                var portfolio = await context.Portfolios.FindAsync(input.PortfolioId);
            
                context.Portfolios.Remove(portfolio);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Портфель удален");
            }
        }
    }
}