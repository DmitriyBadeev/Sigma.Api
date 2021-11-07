using Sigma.Core.Interfaces;

namespace Sigma.Imports.Sber.Common.Factory
{
    public interface IReportParserFactory
    {
        IReportParser<TOperation> GetReportParser<TOperation>()
            where TOperation: IOperation;
    }
}
