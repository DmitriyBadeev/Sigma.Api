using System;
using System.Reflection;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Moex.AssetBuilding.Builders;

namespace Sigma.Integrations.Moex.AssetBuilding
{
    public class AssetBuilderFactory
    {
        public IAssetBuilder<TAsset> GetAssetBuilder<TAsset>()
            where TAsset : IAsset
        {
            Type assetBuilderType = null;

            switch (typeof(TAsset).Name)
            {
                case nameof(Stock):
                    assetBuilderType = typeof(StockBuilder);
                    break;
                case nameof(Fond):
                    assetBuilderType = typeof(FondBuilder);
                    break;
                case nameof(Bond):
                    assetBuilderType = typeof(BondBuilder);
                    break;
            }

            if (assetBuilderType != null)
            {
                var assetBuilder = (IAssetBuilder<TAsset>)Activator.CreateInstance(assetBuilderType);

                return assetBuilder;
            }

            return null;
        }
    }
}
