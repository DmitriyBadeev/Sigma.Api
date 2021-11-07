using System.Collections.Generic;
using System.IO;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;

namespace Sigma.Imports.Sber
{
    public interface IReportParser<TOperation>
        where TOperation: IOperation
    {
        bool TryParse(Stream fileStream, FinanceDbContext context, out List<TOperation> operations, out string errorMessage);
    }
}
