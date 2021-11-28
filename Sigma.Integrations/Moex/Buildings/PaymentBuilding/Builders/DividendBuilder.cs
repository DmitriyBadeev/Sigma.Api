using Sigma.Core.Entities;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Models.Payments.Dividends;
using System.Collections.Generic;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Buildings.Common.Methods;

namespace Sigma.Integrations.Moex.Buildings.PaymentBuilding.Builders
{
    public class DividendBuilder : RequestedBuilder<Dividend, DividendResponse>
    {
        private static Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new()
        {
            { "registryclosedate", ("RegistryCloseDate", MappingMethods.MapPropertyDateTime)},
            { "value", ("Value", MappingMethods.MapPropertyDecimal)},
            { "currencyid", ("CurrencyId", MappingMethods.MapCurrency) }
        };

        public DividendBuilder() 
            : base(_mapRules)
        {
        }

        public override List<Dividend> BuildRequested(DividendResponse response, FinanceDbContext context)
        {
            var dividends = new List<Dividend>();

            var columns = response.dividends.columns;

            foreach (var jsonItems in response.dividends.data)
            {
                var payment = MapRequested(jsonItems, columns, context);
                dividends.Add(payment);
            }

            return dividends;
        }
    }
}
