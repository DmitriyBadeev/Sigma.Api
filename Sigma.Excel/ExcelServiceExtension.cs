using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Entities;
using Sigma.Excel.SberReport;
using Sigma.Excel.SberReport.Common.Factory;
using Sigma.Excel.SberReport.Operations.Asset;
using Sigma.Excel.SberReport.Operations.Currency;

namespace Sigma.Excel
{
    public static class ExcelServiceExtension
    {
        public static IServiceCollection AddExcelServices(this IServiceCollection services)
        {
            // factory
            services.AddTransient<IReportParserFactory, ReportParserFactory>();

            // sber parsers
            services.AddTransient<IReportParser<AssetOperation>, AssetReportParser>();
            services.AddTransient<IReportParser<CurrencyOperation>, CurrencyReportParser>();

            return services;
        }
    }
}
