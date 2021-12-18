using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sigma.Services.Models;

namespace Sigma.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<List<PaymentData>> GetFuturePayments(Guid portfolioId);
        Task<List<PaymentData>> GetFuturePayments(IEnumerable<Guid> portfolioIds);
    }
}