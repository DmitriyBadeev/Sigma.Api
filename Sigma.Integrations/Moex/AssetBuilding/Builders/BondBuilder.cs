using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.Models;

namespace Sigma.Integrations.Moex.AssetBuilding.Builders
{
    public class BondBuilder : IAssetBuilder<Bond>
    {
        private Dictionary<string, string> _mapRules = new();

        public BondBuilder()
        {
            _mapRules.Add("SECID", nameof(Bond.Ticket));
            _mapRules.Add("SHORTNAME", nameof(Bond.ShortName));
            _mapRules.Add("SECID", nameof(Bond.MarketFullName));
            _mapRules.Add("SECID", nameof(Bond.FullName));
            _mapRules.Add("SECID", nameof(Bond.LatName));
            _mapRules.Add("BOARDNAME", nameof(Bond.Description));
            //_mapRules.Add("SECID", nameof(Bond.Percent));
            _mapRules.Add("OFFER", nameof(Bond.Price));
            //_mapRules.Add("PREVWAPRICE", nameof(Bond.PercentChange));
            _mapRules.Add("UPDATETIME", nameof(Bond.UpdateTime));
            _mapRules.Add("SECID", nameof(Bond.AmortizationDate));
            _mapRules.Add("SECID", nameof(Bond.Nominal));
            _mapRules.Add("COUPONVALUE", nameof(Bond.Coupon));
        }

        public List<Bond> BuildAssets(AssetResponse boardJson)
        {
            var bonds = new List<Bond>();

            var securities = boardJson.securities;

            for (int i = 0; i < securities.columns.Count; i++)
            {
                var columnValue = securities.data[i][0].GetString();
            }

            return bonds;
        }
    }
}
