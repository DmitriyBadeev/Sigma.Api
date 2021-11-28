using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Sigma.Infrastructure;

namespace Sigma.Integrations.Moex.Buildings.Common.Methods
{
    public class MappingMethods
    {
        public delegate object MapPropertyFunc(string column,
            List<JsonElement> source,
            List<string> columns,
            FinanceDbContext context);

        public static object MapPropertyString(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var columnsIndex = columns.IndexOf(column);

            if (columnsIndex != -1)
            {
                var property = source[columnsIndex];

                if (property.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                var propertyValue = property.GetString();

                return propertyValue;
            }

            return null;
        }
        public static object MapPropertyInt32(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var columnsIndex = columns.IndexOf(column);

            if (columnsIndex != -1)
            {
                var property = source[columnsIndex];

                if (property.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                var isSuccess = property.TryGetInt32(out var propertyValue);
                if (!isSuccess)
                {
                    return null;
                }

                return propertyValue;
            }

            return null;
        }

        public static object MapPropertyInt64(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var columnsIndex = columns.IndexOf(column);

            if (columnsIndex != -1)
            {
                var property = source[columnsIndex];

                if (property.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                var isSuccess = property.TryGetInt64(out var propertyValue);
                if (!isSuccess)
                {
                    return null;
                }

                return propertyValue;
            }

            return null;
        }

        public static object MapPropertyDecimal(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var columnsIndex = columns.IndexOf(column);

            if (columnsIndex != -1)
            {
                var property = source[columnsIndex];

                if (property.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                var isSuccess = property.TryGetDecimal(out var propertyValue);
                if (!isSuccess)
                {
                    return null;
                }

                return propertyValue;
            }

            return null;
        }

        public static object MapPropertyDateTime(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var columnsIndex = columns.IndexOf(column);

            if (columnsIndex != -1)
            {
                var property = source[columnsIndex];

                if (property.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }

                var isSuccess = property.TryGetDateTime(out var propertyValue);
                if (!isSuccess)
                {
                    return null;
                }

                return propertyValue;
            }

            return null;
        }

        public static object MapUpdateTime(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var updateTime = (string)MapPropertyString("UPDATETIME", source, columns, context);
            var prevDate = (string)MapPropertyString("PREVDATE", source, columns, context);

            if (prevDate == "0000-00-00")
            {
                return null;
            }

            var updateTimeParsed = TimeSpan.Parse(updateTime ?? "00:00:00");
            var prevDateParsed = DateTime.Parse(prevDate ?? "00.00.0000");

            var updateTimeProperty = prevDateParsed + updateTimeParsed;

            return updateTimeProperty;
        }

        public static object MapCurrency(string column, List<JsonElement> source, List<string> columns, FinanceDbContext context)
        {
            var currencyTicket = MapPropertyString(column, source, columns, context);
            if (currencyTicket == null)
            {
                return null;
            }
            
            var currency = context.Currencies.FirstOrDefault(x => x.Ticket == currencyTicket.ToString());

            if (currency == null)
            {
                return null;
            }

            return currency.Id;
        }
    }
}
