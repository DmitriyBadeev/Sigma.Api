using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class StockBuilder : IAssetBuilder<Stock>
    {
        public List<Stock> BuildAssets(AssetResponse boardJson)
        {
            return new List<Stock>();
        }
    }
}
