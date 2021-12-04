using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Models.Assets
{
    public class AssetResponse : IResponse
    {
        public Securities securities { get; set; }
        public MarketData marketdata { get; set; }
    }
}
