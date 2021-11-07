using System.Linq;
using NPOI.SS.UserModel;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;
using Sigma.Infrastructure;

namespace Sigma.Imports.Sber.Common.MappingMethods
{
    public static class ReportMapping
    {
        public delegate object MapFunc(ICell source, FinanceDbContext context, IOperation operation);

        public static object MapDate(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.DateCellValue;

            return value;
        }

        public static object MapString(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.StringCellValue;

            return value;
        }

        public static object MapInt32(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = (int)source.NumericCellValue;

            return value;
        }

        public static object MapDecimal(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = (decimal)source.NumericCellValue;

            return value;
        }

        public static object MapCurrency(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.StringCellValue;

            var currency = context.Currencies.FirstOrDefault(x => x.Ticket == value);

            return currency?.Id;
        }

        public static object MapAssetType(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.StringCellValue;

            AssetType? assetType = null;
            switch (value)
            {
                case "Акция":
                    assetType = AssetType.Stock;
                    break;
                case "Депозитарная расписка":
                    assetType = AssetType.Stock;
                    break;
                case "Облигация":
                    assetType = AssetType.Bond;
                    break;
                case "Пай":
                    assetType = AssetType.Fond;
                    break;
            }

            return assetType;
        }

        public static object MapAssetAction(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.StringCellValue;

            AssetAction? assetAction = null;
            switch (value)
            {
                case "Покупка":
                    assetAction = AssetAction.BuyAction;
                    break;
                case "Продажа":
                    assetAction = AssetAction.SellAction;
                    break;
            }

            return assetAction;
        }

        public static object MapOperationType(ICell source, FinanceDbContext context, IOperation operation)
        {
            var value = source.StringCellValue;

            OperationType? operationType = null;
            switch (value)
            {
                case "Ввод ДС":
                    operationType = OperationType.RefillAction;
                    break;
                case "Зачисление дивидендов":
                    operationType = OperationType.DividendPayment;
                    break;
                case "Зачисление купона":
                    operationType = OperationType.CouponPayment;
                    break;
                case "Вывод ДС":
                    operationType = OperationType.WithdrawalAction;
                    break;
                case "Списание комиссии":
                    operationType = OperationType.Commission;
                    break;
            }

            return operationType;
        }
    }
}
