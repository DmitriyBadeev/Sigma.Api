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
    public class CreateCurrencyOperations
    {
        public record Command(List<CurrencyOperationInput> operations, FinanceDbContext Context, IValidationService ValidationService,
            string UserId) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId) = request;

                var errors = input
                        .Select(x => validationService
                            .NotNegative(x.Total)
                            .NotNegative(x.Amount ?? 0)
                            .CheckEnumValue(x.OperationType)
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

                var operations = input.Select(x => new CurrencyOperation()
                {
                    Amount = x.Amount,
                    PortfolioId = x.PortfolioId,
                    Date = x.Date,
                    Total = x.Total,
                    Ticket = x.Ticket,
                    CurrencyId = x.CurrencyId,
                    OperationType = x.OperationType
                });

                await context.Set<CurrencyOperation>().AddRangeAsync(operations, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return new DefaultPayload(true, "Список операций создан");
            }
        }
    }
}
