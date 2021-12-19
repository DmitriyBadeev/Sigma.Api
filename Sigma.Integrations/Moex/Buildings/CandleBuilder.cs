using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Sigma.Infrastructure;
using Sigma.Integrations.Moex.Buildings.Common;
using Sigma.Integrations.Moex.Buildings.Common.Methods;
using Sigma.Integrations.Moex.Models.Candles;

namespace Sigma.Integrations.Moex.Buildings
{
    public class CandleBuilder : RequestedBuilder<Candle, CandleResponse>
    {
        private static readonly Dictionary<string, (string propertyName, MappingMethods.MapPropertyFunc mapFunc)> _mapRules = new ()
        {
            { "open", (nameof (Candle.Open), MappingMethods.MapPropertyDecimal) },
            { "close", (nameof (Candle.Close), MappingMethods.MapPropertyDecimal) },
            { "high", (nameof (Candle.High), MappingMethods.MapPropertyDecimal) },
            { "low", (nameof (Candle.Low), MappingMethods.MapPropertyDecimal) },
            { "value", (nameof (Candle.Value), MappingMethods.MapPropertyDecimal) },
            { "volume", (nameof (Candle.Volume), MappingMethods.MapPropertyDecimal) },
            { "begin", (nameof (Candle.Begin), MapDate) },
            { "end", (nameof (Candle.End), MapDate) }
        };
        
        public CandleBuilder() : base(_mapRules)
        {
        }
        
        private static object MapDate(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var dateString = (string)MappingMethods.MapPropertyString(column, source, columns, context);

            if (dateString != null)
            {
                return DateTime.Parse(dateString);
            }

            return null;
        }

        public override List<Candle> BuildRequested(CandleResponse response, FinanceDbContext context)
        {
            var columns = response.candles.columns;
            var data = response.candles.data;

            return data
                .Select(jsonItems => MapRequested(jsonItems, columns, context))
                .ToList();
        }
    }
}