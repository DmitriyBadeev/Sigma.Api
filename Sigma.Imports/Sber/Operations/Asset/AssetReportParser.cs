using System.Collections.Generic;
using Sigma.Core.Entities;
using Sigma.Imports.Sber.Common.MappingMethods;

namespace Sigma.Imports.Sber.Operations.Asset
{
    public class AssetReportParser : ReportParser<AssetOperation>
    {
        private const string SHEET_NAME = "Сделки";

        private static readonly Dictionary<string, List<(string property, ReportMapping.MapFunc mapFunc)>> mapRules = new()
        {
            { 
                "Дата заключения", 
                new List<(string property, ReportMapping.MapFunc mapFunc)> 
                {
                    ("Date", ReportMapping.MapDate)
                }
            },
            {
                "Код финансового инструмента", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Ticket", ReportMapping.MapString)
                }
            },
            { 
                "Тип финансового инструмента", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("AssetType", ReportMapping.MapAssetType)
                }
            },
            { 
                "Операция", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("AssetAction", ReportMapping.MapAssetAction)
                }
            },
            { 
                "Количество", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Amount", ReportMapping.MapInt32)
                }
            },
            { 
                "Цена", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Price", ReportMapping.MapDecimal)
                }
            },
            { 
                "Объём сделки",
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("Total", ReportMapping.MapDecimal)
                }
            },
            { 
                "Валюта", 
                new List<(string property, ReportMapping.MapFunc mapFunc)>
                {
                    ("CurrencyId", ReportMapping.MapCurrency)
                }
            }
        };

        public AssetReportParser() : base(SHEET_NAME, mapRules) { }
    }
}
