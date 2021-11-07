using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Api.Mediator.Operations
{
    public class CreateAssetOperation
    {
        public record Command(AssetOperationInput Input, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId, ISynchronizationService SynchronizationService) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (input, context, validationService, userId, synchronizationService) = request;
                
                // TODO Проверить существование тикета
                var error = validationService
                    .NotNegative(input.Amount)
                    .NotNegative(input.Price)
                    .CheckEnumValue(input.AssetAction)
                    .CheckEnumValue(input.AssetType)
                    .CheckExist<Currency>(input.CurrencyId)
                    .CheckExist<Core.Entities.Portfolio>(input.PortfolioId)
                    .PortfolioBelongsUser(input.PortfolioId, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }

                var operation = new AssetOperation
                {
                    Amount = input.Amount,
                    PortfolioId = input.PortfolioId,
                    Date = input.Date,
                    Price = input.Price,
                    Ticket = input.Ticket,
                    AssetAction = input.AssetAction,
                    AssetType = input.AssetType,
                    CurrencyId = input.CurrencyId
                };

                context.AssetOperations.Add(operation);
                
                await context.SaveChangesAsync(cancellationToken);
                await synchronizationService.SyncPortfolio(operation.PortfolioId);
                
                return new DefaultPayload(true, "Операция создана");
            }
        }
    }
}