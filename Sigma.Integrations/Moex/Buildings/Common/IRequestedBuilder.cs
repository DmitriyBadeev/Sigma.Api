using System.Collections.Generic;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Buildings.Common
{
    public interface IRequestedBuilder<TRequested, in TResponse>
        where TRequested : IRequested
        where TResponse : IResponse
    {
        List<TRequested> BuildRequested(TResponse response, FinanceDbContext context);
    }
}
