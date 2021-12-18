using System.Collections.Generic;
using System.IO;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;
using Sigma.Imports.Sber.Common.Factory;
using Sigma.Infrastructure;
using Sigma.Integrations.Common.Enums;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IReportParserFactory _reportParserFactory;
        private readonly FinanceDbContext _context;

        public ExcelService(IReportParserFactory reportParserFactory, FinanceDbContext context)
        {
            _reportParserFactory = reportParserFactory;
            _context = context;
        }

        public bool TryParseReport<TOperation>(
            Stream excelStream, 
            out List<TOperation> operations, 
            out string errorMessage) where TOperation: IOperation
        {
            var reportParser = _reportParserFactory.GetReportParser<TOperation>();

            if (reportParser == null)
            {
                operations = new List<TOperation>();
                errorMessage = "Парсер операций не был найден";

                return false;
            }
            
            var isSuccess = reportParser.TryParse(excelStream, _context, out operations, out errorMessage);

            return isSuccess;
        }

        public void FillAssetOperationData(List<AssetOperation> assetOperations)
        {
            foreach (var assetOperation in assetOperations)
            {
                var currency = _context.Currencies.Find(assetOperation.CurrencyId);
                assetOperation.Currency = currency;

                if (assetOperation.AssetType == AssetType.Bond)
                {
                    assetOperation.Price = assetOperation.Total / assetOperation.Amount;
                }
            }
        }

        public void FillCurrencyOperationData(List<CurrencyOperation> currencyOperations)
        {
            foreach (var currencyOperation in currencyOperations)
            {
                var currency = _context.Currencies.Find(currencyOperation.CurrencyId);

                currencyOperation.Currency = currency;
            }
        }
    }
}
