using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Buildings.Common.Methods;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding;
using Sigma.Integrations.Moex.Models.Assets;

namespace Sigma.Integrations.Moex.Buildings.AssetBuilding.Builders
{
    public class FondBuilder : RequestedBuilder<Fond, AssetResponse>
    {
        private static Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new();

        public FondBuilder() 
            : base(_mapRules)
        {
            _mapRules.TryAdd("SECID", (nameof(Fond.Ticket), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SHORTNAME", (nameof(Fond.ShortName), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SECNAME", (nameof(Fond.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.TryAdd("", (nameof(Fond.FullName), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("LATNAME", (nameof(Fond.LatName), MappingMethods.MapPropertyString));
            //_mapRules.TryAdd("", (nameof(Fond.Description), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("LAST", (nameof(Fond.Price), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("LASTTOPREVPRICE", (nameof(Fond.PriceChange), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("UPDATETIME + PREVDATE", (nameof(Fond.UpdateTime), MappingMethods.MapUpdateTime));
        }

        public override List<Fond> BuildRequested(AssetResponse response, FinanceDbContext context)
        {
            var assets = new List<Fond>();
            var columns = response.securities.columns
                .Concat(response.marketdata.columns)
                .ToList();

            for (int i = 0; i < response.securities.data.Count; i++)
            {
                var data = response.securities.data[i]
                    .Concat(response.marketdata.data[i])
                    .ToList();

                var asset = MapRequested(data, columns, context);
                assets.Add(asset);
            }

            return assets;
        }
    }
}
