using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding
{
    public interface IAssetBuilder<TAsset>
        where TAsset : IAsset
    {
        List<TAsset> BuildAssets(AssetResponse boardJson);
    }
}
