using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.AssetBuilding.Methods;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class BondBuilder : AssetBuilder<Bond>
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

        private object MapPrice(string column, List<JsonElement> source, List<string> columns)
        {
            var last = (decimal?)MappingMethods.MapPropertyDecimal("LAST", source, columns);
            var faceValue = (decimal?) MappingMethods.MapPropertyDecimal("FACEVALUE", source, columns);

            if (last != null && faceValue != null)
            {
                var price = (last / 100) * faceValue; // TODO: Выяснить что такое ACCRUEDINT(nkd)

                return price;
            }

            return null;
        }
    }
}
