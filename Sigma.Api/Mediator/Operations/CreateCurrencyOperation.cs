using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Operations
{
    public class CreateCurrencyOperation
    {
        public record Command(CurrencyOperationInput Input, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId) = request;
                
                // TODO Проверить существование тикета
                var error = validationService
                    .NotNegative(input.Total)
                    .NotNegative(input.Amount ?? 0)
                    .CheckEnumValue(input.OperationType)
                    .CheckExist<Currency>(input.CurrencyId)
                    .CheckExist<Core.Entities.Portfolio>(input.PortfolioId)
                    .PortfolioBelongsUser(input.PortfolioId, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }

                var operation = new CurrencyOperation
                {
                    Amount = input.Amount,
                    PortfolioId = input.PortfolioId,
                    Date = input.Date,
                    Total = input.Total,
                    Ticket = input.Ticket,
                    CurrencyId = input.CurrencyId,
                    OperationType = input.OperationType
                };

                context.CurrencyOperations.Add(operation);
                await context.SaveChangesAsync(cancellationToken);
                
                return new DefaultPayload(true, "Операция создана");
            }
        }
    }
}