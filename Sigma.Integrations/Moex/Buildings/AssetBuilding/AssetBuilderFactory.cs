using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Moex.Buildings.AssetBuilding.Builders;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding;
using System;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Models.Interfaces;

namespace Sigma.Integrations.Moex.Buildings.AssetBuilding
{
    public class AssetBuilderFactory
    {
        public IRequestedBuilder<TAsset, TResponse> GetAssetBuilder<TAsset, TResponse>()
            where TAsset : IAsset, IRequested
            where TResponse : IResponse
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
                var assetBuilder = (IRequestedBuilder<TAsset, TResponse>)Activator.CreateInstance(assetBuilderType);

                return assetBuilder;
            }

            return null;
        }
    }
}
