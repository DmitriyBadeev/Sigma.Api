using System.Collections.Generic;
using System.IO;
using Sigma.Core.Entities;
using Sigma.Core.Interfaces;

namespace Sigma.Services.Interfaces
{
    public interface IExcelService
    {
        bool TryParseReport<TOperation>(
            Stream excelStream,
            out List<TOperation> operations,
            out string errorMessage) where TOperation : IOperation;

        void FillAssetOperationData(List<AssetOperation> assetOperations);
        void FillCurrencyOperationData(List<CurrencyOperation> currencyOperations);
    }
}
