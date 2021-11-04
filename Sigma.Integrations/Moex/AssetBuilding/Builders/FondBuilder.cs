using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.AssetBuilding.Methods;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class FondBuilder : AssetBuilder<Fond>
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
    }
}
