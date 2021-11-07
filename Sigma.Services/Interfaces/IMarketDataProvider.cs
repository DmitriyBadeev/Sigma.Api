using Sigma.Core.Interfaces;

namespace Sigma.Services.Interfaces
{
    public interface IMarketDataProvider
    {
        TAsset GetAsset<TAsset>(string ticket) 
            where TAsset : class, IAsset;
    }
}