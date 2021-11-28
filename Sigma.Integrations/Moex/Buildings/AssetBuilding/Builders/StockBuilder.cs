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
    public class StockBuilder : RequestedBuilder<Stock, AssetResponse>
    {
        private static readonly Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new();

        public StockBuilder()
            : base(_mapRules)
        {
            _mapRules.TryAdd("SECID", (nameof(Stock.Ticket), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SHORTNAME", (nameof(Stock.ShortName), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("SECNAME", (nameof(Stock.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.TryAdd("", nameof(Stock.FullName));
            _mapRules.TryAdd("LATNAME", (nameof(Stock.LatName), MappingMethods.MapPropertyString));
            _mapRules.TryAdd("LOTSIZE", (nameof(Stock.LotSize), MappingMethods.MapPropertyInt32));
            _mapRules.TryAdd("ISSUESIZE", (nameof(Stock.IssueSize), MappingMethods.MapPropertyInt64));
            _mapRules.TryAdd("PREVLEGALCLOSEPRICE", (nameof(Stock.PrevClosePrice), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("IssueSize * Price", (nameof(Stock.Capitalization), MapCapitalization));
            //_mapRules.TryAdd("", nameof(Stock.Description));
            //_mapRules.TryAdd("", nameof(Stock.Sector));
            _mapRules.TryAdd("LAST", (nameof(Stock.Price), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("LASTTOPREVPRICE", (nameof(Stock.PriceChange), MappingMethods.MapPropertyDecimal));
            _mapRules.TryAdd("UPDATETIME + PREVDATE", (nameof(Stock.UpdateTime), MappingMethods.MapUpdateTime));
        }

        private object MapCapitalization(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var issueSize = (long?)MappingMethods.MapPropertyInt64("ISSUESIZE", source, columns, context);
            var price = (decimal?)MappingMethods.MapPropertyDecimal("LAST", source, columns, context);

            if (issueSize != null && price != null)
            {
                return (long)(issueSize * price);
            }

            return null;
        }

        public override List<Stock> BuildRequested(AssetResponse response, FinanceDbContext context)
        {
            var assets = new List<Stock>();
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
