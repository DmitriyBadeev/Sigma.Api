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
    public class RemoveAssetOperation
    {
        public record Command(Guid AssetOperationId, FinanceDbContext Context, IValidationService ValidationService, 
            string UserId, ISynchronizationService SynchronizationService) : IRequest<DefaultPayload>;

        public class Handler : IRequestHandler<Command, DefaultPayload>
        {
            public async Task<DefaultPayload> Handle(Command request, CancellationToken cancellationToken)
            {
                var (assetOperationId, context, validationService, userId, synchronizationService) = request;
                
                var assetOperation = await context.AssetOperations.FindAsync(assetOperationId);
                
                var error = validationService
                    .CheckExist<AssetOperation>(assetOperationId)
                    .PortfolioBelongsUser(assetOperation?.PortfolioId, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload(false, error.Message);
                }
                
                context.AssetOperations.Remove(assetOperation ?? throw new InvalidOperationException("Некорректная валидация"));
                
                await context.SaveChangesAsync(cancellationToken);
                await synchronizationService.SyncPortfolio(assetOperation.PortfolioId);

                return new DefaultPayload(true, "Операция удалена");
            }
        }
    }
}