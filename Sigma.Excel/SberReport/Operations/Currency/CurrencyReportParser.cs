using System;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;
using Sigma.Excel.SberReport.Common.MappingMethods;
using Sigma.Infrastructure;

namespace Sigma.Excel.SberReport.Operations.Currency
{
    public class CurrencyReportParser : ReportParser<CurrencyOperation>
    {
        private const string SHEET_NAME = "Движение ДС";
        private static readonly Dictionary<string, List<(string property, ReportMapping.MapFunc mapFunc)>> mapRules = new()
        {
            {
                "Сумма", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Total", ReportMapping.MapDecimal)
                }
            },
            {
                "Дата исполнения поручения",
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Date" , ReportMapping.MapDate)
                }
            },
            {
                "Операция",
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("OperationType" , ReportMapping.MapOperationType)
                }
            },
            {
                "Валюта операции",
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("CurrencyId" , ReportMapping.MapCurrency)
                }
            },
            {
                "Содержание операции", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Ticket", MapTicket),
                    ("Amount", MapAmount)
                }
            }
        };

        public CurrencyReportParser() : base(SHEET_NAME, mapRules) { }

        private static object MapTicket(ICell source, FinanceDbContext context, IOperation operation)
        {
            var currencyOperation = (CurrencyOperation) operation;

            if (currencyOperation.OperationType == OperationType.CouponPayment
                || currencyOperation.OperationType == OperationType.DividendPayment)
            {
                var value = source.StringCellValue;

                var asset = (IAsset)context.Stocks.FirstOrDefault(x => value.ToLower().Contains(x.ShortName.ToLower())) ??
                                   context.Bonds.FirstOrDefault(x => value.ToLower().Contains(x.ShortName.ToLower()));

                return asset?.Ticket;
            }

            return null;
        }

        private static object MapAmount(ICell source, FinanceDbContext context, IOperation operation)
        {
            var currencyOperation = (CurrencyOperation)operation;

            if (currencyOperation.OperationType == OperationType.CouponPayment
                || currencyOperation.OperationType == OperationType.DividendPayment)
            {
                var value = source.StringCellValue;

                var splitValue = value.Split(' ');

                var indexOfAmount = Array.IndexOf(splitValue, "шт.");
                if (indexOfAmount == -1)
                {
                    return null;
                }

                return int.Parse(splitValue[indexOfAmount - 1]);
            }

            return null;
        }
    }
}
