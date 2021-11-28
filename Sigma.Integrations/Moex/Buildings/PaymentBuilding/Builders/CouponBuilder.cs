using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Models.Payments.Coupons;
using System.Collections.Generic;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Buildings.Common.Methods;

namespace Sigma.Integrations.Moex.Buildings.PaymentBuilding.Builders
{
    public class CouponBuilder : RequestedBuilder<Coupon, CouponResponse>
    {
        private static Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new ()
        {
            { "coupondate", ("CouponDate", MappingMethods.MapPropertyDateTime) },
            { "value", ("Value", MappingMethods.MapPropertyDecimal) },
            { "valueprc", ("ValuePercent", MappingMethods.MapPropertyDecimal) },
            { "faceunit", ("CurrencyId", MappingMethods.MapCurrency) }
        };

        public CouponBuilder() 
            : base(_mapRules)
        {
        }

        public override List<Coupon> BuildRequested(CouponResponse response, FinanceDbContext context)
        {
            var coupons = new List<Coupon>();

            var columns = response.coupons.columns;
            var data = response.coupons.data;
            foreach (var jsonItems in data)
            {
                var payment = MapRequested(jsonItems, columns, context);
                coupons.Add(payment);
            }

            columns = response.amortizations.columns;
            data = response.amortizations.data;
            foreach (var jsonItems in data)
            {
                var payment = MapRequested(jsonItems, columns, context);
                coupons.Add(payment);
            }

            return coupons;
        }
    }
}
