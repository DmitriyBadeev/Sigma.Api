using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;

namespace Sigma.Api.Mediator.Operations
{
    public class RemoveCurrencyOperation
    {
        public record Command(Guid CurrencyOperationId, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (currencyOperationId, context, validationService, userId) = request;
                
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

                return new DefaultPayload(true, "Операция удалена");
            }
        }
    }
}