using Sigma.Core.Interfaces;
using Sigma.Infrastructure;
using Sigma.Integrations.Common.Enums;
using Sigma.Integrations.Moex.Buildings.AssetBuilding;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding;
using Sigma.Integrations.Moex.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sigma.Integrations.Moex
{
    public class MoexIntegrationService
    {
        private readonly FinanceDbContext _context;
        private readonly MoexApi _moexApi;
        private readonly AssetBuilderFactory _assetBuilderFactory;
        private readonly PaymentBuilderFactory _paymentBuilderFactory;

        public MoexIntegrationService(
            FinanceDbContext context,
            MoexApi moexApi, 
            AssetBuilderFactory assetBuilderFactory, 
            PaymentBuilderFactory paymentBuilderFactory)
        {
            _context = context;
            _moexApi = moexApi;
            _assetBuilderFactory = assetBuilderFactory;
            _paymentBuilderFactory = paymentBuilderFactory;
        }

        public async Task<List<TAsset>> GetAssets<TAsset, TResponse>()
            where TAsset : IAsset, IRequested
            where TResponse : IResponse
        {
            var assetType = Enum.Parse<AssetTypes>(typeof(TAsset).Name);
            
            var assetJson = await _moexApi.GetAssetJson(assetType);
            var assetDeserialized = JsonSerializer.Deserialize<TResponse>(assetJson);

            var assetBuilder = _assetBuilderFactory.GetAssetBuilder<TAsset, TResponse>();

            var assets = assetBuilder.BuildRequested(assetDeserialized, _context);
            
            return assets;
        }

        public async Task<List<TPayment>> GetPayments<TPayment, TResponse>(string ticket)
            where TPayment : IPayment, IRequested
            where TResponse : IResponse
        {
            var paymentType = Enum.Parse<PaymentTypes>(typeof(TPayment).Name);

            var paymentJson = await _moexApi.GetPaymentJson(paymentType, ticket);
            var paymentDeserialized = JsonSerializer.Deserialize<TResponse>(paymentJson);

            var paymentBuilder = _paymentBuilderFactory.GetPaymentBuilder<TPayment, TResponse>();

            var payments = paymentBuilder.BuildRequested(paymentDeserialized, _context);

            return payments;
        }
    }
}