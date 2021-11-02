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
            _mapRules.Add("SECID", (nameof(Fond.Ticket), MappingMethods.MapPropertyString));
            _mapRules.Add("SHORTNAME", (nameof(Fond.ShortName), MappingMethods.MapPropertyString));
            _mapRules.Add("SECNAME", (nameof(Fond.MarketFullName), MappingMethods.MapPropertyString));
            //_mapRules.Add("", (nameof(Fond.FullName), MappingMethods.MapPropertyString));
            _mapRules.Add("LATNAME", (nameof(Fond.LatName), MappingMethods.MapPropertyString));
            //_mapRules.Add("", (nameof(Fond.Description), MappingMethods.MapPropertyString));
            _mapRules.Add("LAST", (nameof(Fond.Price), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("LASTTOPREVPRICE", (nameof(Fond.PriceChange), MappingMethods.MapPropertyDecimal));
            _mapRules.Add("UPDATETIME + PREVDATE", (nameof(Fond.UpdateTime), MappingMethods.MapUpdateTime));
        }
    }
}
