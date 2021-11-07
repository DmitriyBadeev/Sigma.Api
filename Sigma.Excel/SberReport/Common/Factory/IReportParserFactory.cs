using Sigma.Core.Interfaces;

namespace Sigma.Excel.SberReport.Common.Factory
{
    public interface IReportParserFactory
    {
        IReportParser<TOperation> GetReportParser<TOperation>()
            where TOperation: IOperation;
    }
}
