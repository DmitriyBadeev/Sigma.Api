using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Sigma.Core.Interfaces;
using Sigma.Integrations.Common.Enums;
using Sigma.Integrations.Moex.AssetBuilding;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex
{
    public class MoexIntegrationService
    {
        private readonly MoexApi _moexApi;
        private readonly AssetBuilderFactory _assetBuilderFactory;

        public MoexIntegrationService(MoexApi moexApi, AssetBuilderFactory assetBuilderFactory)
        {
            _moexApi = moexApi;
            _assetBuilderFactory = assetBuilderFactory;
        }

        public async Task<List<TAsset>> GetAssets<TAsset>()
            where TAsset : IAsset
        {
            var tradeMode = Enum.Parse<MoexTradeModes>(typeof(TAsset).Name);
            
            var boardJson = await _moexApi.GetBoardJson(tradeMode);
            var assetJson = JsonSerializer.Deserialize<AssetResponse>(boardJson);

            var assetBuilder = _assetBuilderFactory.GetAssetBuilder<TAsset>();

            var assets = assetBuilder.BuildAssets(assetJson);
            
            return assets;
        }
    }
}