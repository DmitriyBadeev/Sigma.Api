using System.Collections.Generic;
using System.IO;
using Sigma.Core.Interfaces;
using Sigma.Imports.Sber.Common.Factory;
using Sigma.Infrastructure;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IReportParserFactory reportParserFactory;
        private readonly FinanceDbContext context;

        public ExcelService(IReportParserFactory reportParserFactory, FinanceDbContext context)
        {
            this.reportParserFactory = reportParserFactory;
            this.context = context;
        }

        public bool TryParseReport<TOperation>(
            Stream excelStream, 
            out List<TOperation> operations, 
            out string errorMessage) where TOperation: IOperation
        {
            var reportParser = reportParserFactory.GetReportParser<TOperation>();

            if (reportParser == null)
            {
                operations = new List<TOperation>();
                errorMessage = "Парсер операций не был найден";

                return false;
            }
            
            var isSuccess = reportParser.TryParse(excelStream, context, out operations, out errorMessage);

            return isSuccess;
        }
    }
}
