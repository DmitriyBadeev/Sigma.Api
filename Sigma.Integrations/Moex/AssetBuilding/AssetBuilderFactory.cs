using System;
using Sigma.Core.Interfaces;

namespace Sigma.Integrations.Moex.AssetBuilding
{
    public class AssetBuilderFactory
    {
        public IAssetBuilder<TAsset> GetAssetBuilder<TAsset>()
            where TAsset : IAsset
        {
            var assetBuilder = (IAssetBuilder<TAsset>) Activator.CreateInstance(typeof(IAssetBuilder<TAsset>));

            return assetBuilder;
        }
    }
}
