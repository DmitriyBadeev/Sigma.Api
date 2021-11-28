using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Portfolios
{
    public static class CreatePortfolio
    {
        public record Command(AddPortfolioInput Input, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId) : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId) = request;

                var error = validationService
                    .CheckExist<PortfolioType>(input.TypeId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }
            
                var portfolio = new Core.Entities.Portfolio{
                    Name = input.Name,
                    UserId = userId,
                    PortfolioTypeId = input.TypeId
                };

                context.Portfolios.Add(portfolio);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Портфель создан");
            }
        }
    }
}