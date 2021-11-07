using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sigma.Core.Interfaces;

namespace Sigma.Services.Interfaces
{
    public interface IExcelService
    {
        bool TryParseReport<TOperation>(
            Stream excelStream,
            out List<TOperation> operations,
            out string errorMessage) where TOperation : IOperation;
    }
}
