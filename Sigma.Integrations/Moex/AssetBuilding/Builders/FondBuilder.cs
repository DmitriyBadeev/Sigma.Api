using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class FondBuilder : IAssetBuilder<Fond>
    {
        public List<Fond> BuildAssets(AssetResponse boardJson)
        {
            throw new NotImplementedException();
        }
    }
}
