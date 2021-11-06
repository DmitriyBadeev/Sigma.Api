using System.Linq;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services.SynchronizationService
{
    public class MarketDataProvider : IMarketDataProvider
    {
        private readonly FinanceDbContext _context;

        public MarketDataProvider(FinanceDbContext context)
        {
            _context = context;
        }

        public TAsset GetAsset<TAsset>(string ticket) 
            where TAsset : class, IAsset
        {
            return _context
                .Set<TAsset>()
                .FirstOrDefault(a => a.Ticket == ticket);
        }
    }
}