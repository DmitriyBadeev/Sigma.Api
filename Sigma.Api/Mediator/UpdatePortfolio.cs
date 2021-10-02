using System.Threading;
using System.Threading.Tasks;
using Sigma.Infrastructure;
using MediatR;
using Sigma.Api.GraphQL;

namespace Sigma.Api.Mediator
{
    public static class UpdatePortfolio
    {
        public record Command(UpdatePortfolioInput Input, FinanceDbContext Context) : IRequest<DefaultPayload>;
    
        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context) = request;

                var portfolio = await context.Portfolios.FindAsync(input.PortfolioId);
                var portfolioType = await context.PortfolioTypes.FindAsync(input.TypeId);
            
                if (portfolio == null)
                {
                    return new DefaultPayload(false, "Потфель не найден");
                }

                if (portfolioType == null)
                {
                    return new DefaultPayload(false, "Тип портфеля не найден");
                }

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