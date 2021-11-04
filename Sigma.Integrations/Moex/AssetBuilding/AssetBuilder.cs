using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Moex.AssetBuilding.Methods;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding
{
    public abstract class AssetBuilder<TAsset> : IAssetBuilder<TAsset>
        where TAsset : IAsset
    {
        private readonly Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules;

        protected AssetBuilder(Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> mapRules)
        {
            _mapRules = mapRules;
        }

        public List<TAsset> BuildAssets(AssetResponse boardJson)
        {
            var assets = new List<TAsset>();
            var columns = boardJson.securities.columns
                .Concat(boardJson.marketdata.columns)
                .ToList();

            for (int i = 0; i < boardJson.securities.data.Count; i++)
            {
                var data = boardJson.securities.data[i]
                    .Concat(boardJson.marketdata.data[i])
                    .ToList();

                var asset = MapAsset(data, columns);
                assets.Add(asset);
            }

            return assets;
        }

        protected virtual TAsset MapAsset(List<JsonElement> json, List<string> columns)
        {
            var asset = (TAsset) Activator.CreateInstance(typeof(TAsset));
            var assetType = asset.GetType();

            foreach (var mapRule in _mapRules)
            {
                var assetProperty = assetType.GetProperty(mapRule.Value.propertyName);
                if (assetProperty == null)
                {
                    continue;
                }

                var property = mapRule.Value.mapFunc(mapRule.Key, json, columns);
                if (property == null)
                {
                    continue;
                }

                assetProperty.SetValue(asset, property);
            }

            return asset;
        }
    }
}
