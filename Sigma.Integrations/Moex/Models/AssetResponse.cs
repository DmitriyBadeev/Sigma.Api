using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigma.Integrations.Moex.Models
{
    public class AssetResponse
    {
        public Securities securities { get; set; }
        public MarketData marketdata { get; set; }
    }
}
