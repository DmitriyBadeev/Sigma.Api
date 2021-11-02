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
        private static Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new();

        public StockBuilder()
            : base(_mapRules)
        {
            _mapRules.Add("SECID", (nameof(Stock.Ticket), MappingMethods.MapPropertyString));
            _mapRules.Add("SHORTNAME", (nameof(Stock.ShortName), MappingMethods.MapPropertyString));
            _mapRules.Add("SECNAME", (nameof(Stock.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.Add("", nameof(Stock.FullName));
            _mapRules.Add("LATNAME", (nameof(Stock.LatName), MappingMethods.MapPropertyString));
            _mapRules.Add("LOTSIZE", (nameof(Stock.LotSize), MappingMethods.MapPropertyInt32));
            _mapRules.Add("ISSUESIZE", (nameof(Stock.IssueSize), MappingMethods.MapPropertyInt64));
            _mapRules.Add("PREVLEGALCLOSEPRICE", (nameof(Stock.PrevClosePrice), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("IssueSize * Price", (nameof(Stock.Capitalization), MapCapitalization));
            //_mapRules.Add("", nameof(Stock.Description));
            //_mapRules.Add("", nameof(Stock.Sector));
            _mapRules.Add("LAST", (nameof(Stock.Price), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("LASTTOPREVPRICE", (nameof(Stock.PriceChange), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("UPDATETIME + PREVDATE", (nameof(Stock.UpdateTime), MappingMethods.MapUpdateTime));
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
