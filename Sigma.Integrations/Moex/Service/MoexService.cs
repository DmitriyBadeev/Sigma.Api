﻿using System;
using System.Text.Json;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Common.Enums;
using Sigma.Integrations.Moex.AssetBuilding;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.Service
{
    public class MoexService : IMoexService
    {
        private readonly FinanceDbContext _context;
        private readonly MoexApi _moexApi;
        private readonly AssetBuilderFactory _assetBuilderFactory;

        public MoexService(FinanceDbContext context, MoexApi moexApi, AssetBuilderFactory assetBuilderFactory)
        {
            _context = context;
            _moexApi = moexApi;
            _assetBuilderFactory = assetBuilderFactory;
        }

        public void RefreshBoards()
        {
            RefreshBoard<Stock>();
            RefreshBoard<Fond>();
            RefreshBoard<Bond>();

            _context.SaveChanges();
        }

        private void RefreshBoard<TAsset>()
            where TAsset : IAsset
        {
            var tradeMode = Enum.Parse<MoexTradeModes>(nameof(TAsset));

            var boardJson = _moexApi.GetBoardJson(tradeMode).Result;

            SaveToContext<TAsset>(boardJson);
        }

        private void SaveToContext<TAsset>(string boardJson)
            where TAsset : IAsset
        {
            var assetJson = JsonSerializer.Deserialize<AssetResponse>(boardJson);

            var assetBuilder = _assetBuilderFactory.GetAssetBuilder<TAsset>();

            var assets = assetBuilder.BuildAssets(assetJson);

            _context.AddRange(assets);
        }
    }
}