using System.Collections.Generic;
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
    public class CreateAssetOperations
    {
        public record Command(List<AssetOperationInput> operations, FinanceDbContext Context, IValidationService ValidationService,
            string UserId) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId) = request;

                var errors = input
                        .Select(x => validationService
                            .NotNegative(x.Amount)
                            .NotNegative(x.Price)
                            .CheckEnumValue(x.AssetAction)
                            .CheckEnumValue(x.AssetType)
                            .CheckExist<Currency>(x.CurrencyId)
                            .CheckExist<Core.Entities.Portfolio>(x.PortfolioId)
                            .PortfolioBelongsUser(x.PortfolioId, userId)
                            .FirstError)
                        .ToList();

                if (errors.Any(x => x != null))
                {
                    var notNullErrors = errors.Where(x => x != null);

                    var errorMessage = string.Join("\n\n", notNullErrors.Select(x => x.Message));

                    return new DefaultPayload(false, errorMessage);
                }

                var operations = input.Select(x => new AssetOperation
                {
                    Amount = x.Amount,
                    PortfolioId = x.PortfolioId,
                    Date = x.Date,
                    Price = x.Price,
                    Ticket = x.Ticket,
                    AssetAction = x.AssetAction,
                    AssetType = x.AssetType,
                    CurrencyId = x.CurrencyId
                });

                await context.Set<AssetOperation>().AddRangeAsync(operations, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Список операций создан");
            }
        }
    }
}
