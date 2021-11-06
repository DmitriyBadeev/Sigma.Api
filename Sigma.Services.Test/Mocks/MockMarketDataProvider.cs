using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Test.Mocks
{
    public class MockMarketDataProvider : IMarketDataProvider
    {
        private IEnumerable<IAsset> _assets = new IAsset[]
        {
            new Stock
            {
                Ticket = "YNDX",
                Price = 3000
            },
            new Stock
            {
                Ticket = "SBER",
                Price = 200
            },
            new Fond
            {
                Ticket = "FXGD",
                Price = 2000
            },
            new Bond
            {
                Ticket = "SU000",
                Price = 1000
            }
        };
        
        public TAsset GetAsset<TAsset>(string ticket) 
            where TAsset : class, IAsset
        {
            return (TAsset) _assets.FirstOrDefault(a => a.Ticket == ticket);
        }
    }
}