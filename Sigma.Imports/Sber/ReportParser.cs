using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Sigma.Core.Interfaces;
using Sigma.Imports.Sber.Common.MappingMethods;
using Sigma.Infrastructure;

namespace Sigma.Imports.Sber
{
    public class ReportParser<TOperation> : IReportParser<TOperation>
        where TOperation : IOperation
    {
        private readonly string SHEET_NAME = "";

        private readonly Dictionary<string, List<(string property, ReportMapping.MapFunc mapFunc)>> mapRules;

        public ReportParser(string sheetName, Dictionary<string, List<(string property, ReportMapping.MapFunc mapFunc)>> mapRules)
        {
            SHEET_NAME = sheetName;
            this.mapRules = mapRules;
        }

        public bool TryParse(Stream fileStream, FinanceDbContext context, out List<TOperation> operations, out string errorMessage)
        {
            errorMessage = null;
            operations = new List<TOperation>();
            IWorkbook book;

            // .xlsx trying
            try
            {
                book = new XSSFWorkbook(fileStream);
            }
            catch(Exception e)
            {
                errorMessage = e.Message;

                return false;
            }

            operations = ParseReport(book, context);

            return true;
        }

        protected virtual List<TOperation> ParseReport(IWorkbook workbook, FinanceDbContext context)
        {
            var operations = new List<TOperation>();

            var sheet = workbook.GetSheet(SHEET_NAME);

            var titleRow = sheet.GetRow(0);

            var rowNum = 1;
            while (true)
            {
                var operation = (TOperation)Activator.CreateInstance(typeof(TOperation));
                var operationType = operation.GetType();

                var row = sheet.GetRow(rowNum);

                if (row == null)
                {
                    break;
                }

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    var cell = row.Cells[i];

                    var title = titleRow.Cells[i].StringCellValue;
                    if (mapRules.Keys.Contains(title))
                    {
                        var mapRule = mapRules[title];

                        foreach (var mapRuleItem in mapRule)
                        {
                            var operationProperty = operationType.GetProperty(mapRuleItem.property);
                            if (operationProperty != null)
                            {
                                var value = mapRuleItem.mapFunc(cell, context, operation);
                                if (value != null)
                                {
                                    operationProperty.SetValue(operation, value);
                                }
                            }
                        }
                    }
                }

                operations.Add(operation);

                rowNum++;
            }

            return operations;
        }
    }
}
