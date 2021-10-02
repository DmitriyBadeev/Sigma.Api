using System.Threading;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.GraphQL;

namespace Sigma.Api.Mediator
{
    public static class CreatePortfolio
    {
        public record Command(AddPortfolioInput Input, FinanceDbContext Context) : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context) = request;
            
                var portfolioType = await context.PortfolioTypes.FindAsync(input.TypeId);

                if (portfolioType == null)
                {
                    return new DefaultPayload(false, "Неверный тип портфеля");
                }
            
                var portfolio = new Portfolio{
                    Name = input.Name,
                    UserId = input.UserId,
                    PortfolioTypeId = portfolioType.Id
                };

                context.Portfolios.Add(portfolio);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Портфель создан");
            }
        }
    }
}