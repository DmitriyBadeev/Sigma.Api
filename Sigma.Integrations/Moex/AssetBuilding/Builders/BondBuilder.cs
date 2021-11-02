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
            _mapRules.Add("SECID", (nameof(Bond.Ticket), MappingMethods.MapPropertyString));
            _mapRules.Add("SHORTNAME", (nameof(Bond.ShortName), MappingMethods.MapPropertyString));
            _mapRules.Add("SECNAME", (nameof(Bond.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.Add("", nameof(Bond.FullName));
            _mapRules.Add("LATNAME", (nameof(Bond.LatName), MappingMethods.MapPropertyString));
            //_mapRules.Add("", nameof(Bond.Description));
            _mapRules.Add("LAST", (nameof(Bond.Percent), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("(LAST(percent) / 100) * FACEVALUE(nominal) + ACCRUEDINT(nkd)", (nameof(Bond.Price), MapPrice));
            _mapRules.Add("LASTTOPREVPRICE", (nameof(Bond.PercentChange), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("UPDATETIME + PREVDATE", (nameof(Bond.UpdateTime), MappingMethods.MapUpdateTime));
            _mapRules.Add("MATDATE", (nameof(Bond.AmortizationDate), MappingMethods.MapPropertyDateTime));
            _mapRules.Add("FACEVALUE", (nameof(Bond.Nominal), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("COUPONVALUE", (nameof(Bond.Coupon), MappingMethods.MapPropertyDecimal));
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
