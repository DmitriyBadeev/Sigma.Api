using Microsoft.Extensions.DependencyInjection;
using Sigma.Core.Entities;
using Sigma.Imports.Sber;
using Sigma.Imports.Sber.Common.Factory;
using Sigma.Imports.Sber.Operations.Asset;
using Sigma.Imports.Sber.Operations.Currency;

namespace Sigma.Imports
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
