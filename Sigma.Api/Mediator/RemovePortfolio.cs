using System.Threading;
using System.Threading.Tasks;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.GraphQL;

namespace Sigma.Api.Mediator
{
    public static class RemovePortfolio
    {
        public record Command(RemovePortfolioInput Input, FinanceDbContext Context) : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context) = request;

                var portfolio = await context.Portfolios.FindAsync(input.PortfolioId);

                if (portfolio == null)
                {
                    return new DefaultPayload(false, "Потфель не найден");
                }
            
                context.Portfolios.Remove(portfolio);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Портфель удален");
            }
        }
    }
}