using System;
using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Infrastructure.Services;

namespace Sigma.Services.Services.SynchronizationService
{
    public class CurrencyOperationHandler
    {
        private static readonly Func<decimal, decimal, decimal> MinusFunc = (a, b) => a - b;
        private static readonly Func<decimal, decimal, decimal> PlusFunc = (a, b) => a + b;
        private static readonly Func<decimal, decimal, decimal> SafeDivFunc = (a, b) => b != 0 ? a / b : 0;

        public PortfolioParameters Handle(PortfolioParameters initParameters, IEnumerable<CurrencyOperation> operations)
        {
            return operations.Aggregate(initParameters, Handle);
        }

        private PortfolioParameters Handle(PortfolioParameters initParameters, CurrencyOperation operation)
        {
            switch (operation.OperationType)
            {
                case OperationType.RefillAction:
                    return AccountActionHandle(initParameters, operation, PlusFunc);
                case OperationType.WithdrawalAction:
                    return AccountActionHandle(initParameters, operation, MinusFunc);;
                case OperationType.DividendPayment:
                    return PaymentActionHandle(initParameters, operation);
                case OperationType.CouponPayment:
                    return PaymentActionHandle(initParameters, operation);
                case OperationType.Commission:
                    return CommissionHandle(initParameters, operation);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private PortfolioParameters AccountActionHandle(PortfolioParameters parameters, CurrencyOperation operation, 
            Func<decimal, decimal, decimal> sumFunc)
        {
            var withNewBalance = BalanceHandle(parameters, operation, sumFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            var newInvestSum = sumFunc(parameters.InvestedSum, rubTotal);
            
            return withNewBalance with
            {
                InvestedSum = newInvestSum,
                Cost = sumFunc(parameters.Cost, rubTotal),
                DividendProfitPercent = Math.Abs(SafeDivFunc(parameters.DividendProfit, newInvestSum))
            };
        }

        private PortfolioParameters PaymentActionHandle(PortfolioParameters parameters, CurrencyOperation operation)
        {
            var withNewBalance = BalanceHandle(parameters, operation, PlusFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            var newDividendProfit = parameters.DividendProfit + rubTotal;
            return withNewBalance with
            {
                Cost = parameters.Cost + rubTotal,
                DividendProfit = newDividendProfit,
                DividendProfitPercent = Math.Abs(SafeDivFunc(newDividendProfit, parameters.InvestedSum)),
            };
        }

        private PortfolioParameters CommissionHandle(PortfolioParameters parameters, CurrencyOperation operation)
        {
            var withNewBalance = BalanceHandle(parameters, operation, MinusFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            
            return withNewBalance with
            {
                Cost = parameters.Cost - rubTotal,
            };
        }

        private PortfolioParameters BalanceHandle(PortfolioParameters parameters, CurrencyOperation operation,
            Func<decimal, decimal, decimal> sumFunc)
        {
            if (operation.Currency.Ticket == SeedFinanceData.RUB_TICKET)
            {
                return parameters with {RubBalance = sumFunc(parameters.RubBalance, operation.Total)};
            }

            if (operation.Currency.Ticket == SeedFinanceData.EURO_TICKET)
            {
                return parameters with {EuroBalance = sumFunc(parameters.EuroBalance, operation.Total)};
            }

            if (operation.Currency.Ticket == SeedFinanceData.DOLLAR_TICKET)
            {
                return parameters with {DollarBalance = sumFunc(parameters.DollarBalance, operation.Total)};
            }

            return parameters;
        }
    }
}