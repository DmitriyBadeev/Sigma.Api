using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Buildings.Common.Methods;
using Sigma.Integrations.Moex.Buildings.PaymentBuilding;
using Sigma.Integrations.Moex.Models.Assets;

namespace Sigma.Integrations.Moex.Buildings.AssetBuilding.Builders
{
    public class BondBuilder : RequestedBuilder<Bond, AssetResponse>
    {
        private static Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new();

        public BondBuilder() 
            : base(_mapRules)
        {
            _mapRules.TryAdd("SECID", (nameof(Bond.Ticket), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SHORTNAME", (nameof(Bond.ShortName), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SECNAME", (nameof(Bond.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.TryAdd("", nameof(Bond.FullName));
            _mapRules.TryAdd("LATNAME", (nameof(Bond.LatName), MappingMethods.MapPropertyString));
            //_mapRules.TryAdd("", nameof(Bond.Description));
            _mapRules.TryAdd("LAST", (nameof(Bond.Percent), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("(LAST(percent) / 100) * FACEVALUE(nominal) + ACCRUEDINT(nkd)", (nameof(Bond.Price), MapPrice));
            _mapRules.TryAdd("LASTTOPREVPRICE", (nameof(Bond.PercentChange), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("UPDATETIME + PREVDATE", (nameof(Bond.UpdateTime), MappingMethods.MapUpdateTime));
            _mapRules.TryAdd("MATDATE", (nameof(Bond.AmortizationDate), MappingMethods.MapPropertyDateTime));
            _mapRules.TryAdd("FACEVALUE", (nameof(Bond.Nominal), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("COUPONVALUE", (nameof(Bond.Coupon), MappingMethods.MapPropertyDecimal));
        }

        private object MapPrice(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var last = (decimal?)MappingMethods.MapPropertyDecimal("LAST", source, columns, context);
            var faceValue = (decimal?) MappingMethods.MapPropertyDecimal("FACEVALUE", source, columns, context);

            if (last != null && faceValue != null)
            {
                var price = (last / 100) * faceValue; // TODO: Выяснить что такое ACCRUEDINT(nkd)

                return price;
            }

            return null;
        }

        public override List<Bond> BuildRequested(AssetResponse response, FinanceDbContext context)
        {
            var assets = new List<Bond>();
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
