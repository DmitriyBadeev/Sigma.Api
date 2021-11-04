using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.AssetBuilding.Methods;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class StockBuilder : AssetBuilder<Stock>
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

        private object MapCapitalization(string column, List<JsonElement> source, List<string> columns)
        {
            var issueSize = (long?)MappingMethods.MapPropertyInt64("ISSUESIZE", source, columns);
            var price = (decimal?)MappingMethods.MapPropertyDecimal("LAST", source, columns);

            if (issueSize != null && price != null)
            {
                return (long)(issueSize * price);
            }

            return null;
        }
    }
}
