using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sigma.Api.GraphQL;
using Sigma.Api.Validations.Interfaces;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Api.Mediator.Portfolios
{
    public class GetFuturePayments
    {
        public record Query(IValidationService ValidationService, IEnumerable<Guid> PortfolioIds, string UserId, 
            FinanceDbContext Context, IPaymentService PaymentService) : IRequest<DefaultPayload<List<PaymentData>>>;
    
        public class Handler : IRequestHandler<Query, DefaultPayload<List<PaymentData>>>
        {
            public async Task<DefaultPayload<List<PaymentData>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var (validationService, portfolioIds, userId, context, paymentService) = request;

                var ids = portfolioIds.ToList();
                var error = validationService
                    .CheckExist<Portfolio>(ids)
                    .PortfoliosBelongUser(ids, userId)
                    .FirstError;

                if (error != null)
                {
                    return new DefaultPayload<List<PaymentData>>(false, error.Message);
                }

                var payments = await paymentService.GetFuturePayments(ids);

                return new DefaultPayload<List<PaymentData>>(true, "Будущие выплаты получены", payments);
            }
        }
    }
}