using System;
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
    public class RemoveCurrencyOperation
    {
        public record Command(Guid CurrencyOperationId, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId, ISynchronizationService SynchronizationService) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (currencyOperationId, context, validationService, userId, synchronizationService) = request;
                
                var currencyOperation = await context.CurrencyOperations.FindAsync(currencyOperationId);
                
                var error = validationService
                    .CheckExist<CurrencyOperation>(currencyOperationId)
                    .PortfolioBelongsUser(currencyOperation?.PortfolioId, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }
                
                context.CurrencyOperations.Remove(currencyOperation ?? throw new InvalidOperationException("Некорректная валидация"));
                
                await context.SaveChangesAsync(cancellationToken);
                await synchronizationService.SyncPortfolio(currencyOperation.PortfolioId);

                return new DefaultPayload(true, "Операция удалена");
            }
        }
    }
}