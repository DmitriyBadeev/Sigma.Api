using System;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;
using Sigma.Imports.Sber.Operations.Asset;
using Sigma.Imports.Sber.Operations.Currency;

namespace Sigma.Imports.Sber.Common.Factory
{
    public class ReportParserFactory : IReportParserFactory
    {
        public IReportParser<TOperation> GetReportParser<TOperation>()
            where TOperation: IOperation
        {
            Type reportParserType = null;

            switch (typeof(TOperation).Name)
            {
                case nameof(AssetOperation):
                    reportParserType = typeof(AssetReportParser);
                    break;
                case nameof(CurrencyOperation):
                    reportParserType = typeof(CurrencyReportParser);
                    break;
            }

            if (reportParserType != null)
            {
                var reportParser = (IReportParser<TOperation>)Activator.CreateInstance(reportParserType);

                return reportParser;
            }

            return null;
        }
    }
}
