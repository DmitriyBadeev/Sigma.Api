using System;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Infrastructure.Services;

namespace Sigma.Services.Services
{
    public class CurrencyOperationHandler
    {
        private static readonly Func<decimal, decimal, decimal> MinusFunc = (a, b) => a - b;
        private static readonly Func<decimal, decimal, decimal> PlusFunc = (a, b) => a + b;
        
        public PortfolioParameters Handle(PortfolioParameters initParameters, CurrencyOperation operation)
        {
            switch (operation.OperationType)
            {
                case OperationType.RefillAction:
                    return AccountActionHandler(initParameters, operation, PlusFunc);
                case OperationType.WithdrawalAction:
                    return AccountActionHandler(initParameters, operation, MinusFunc);;
                case OperationType.DividendPayment:
                    return PaymentActionHandler(initParameters, operation);
                case OperationType.CouponPayment:
                    return PaymentActionHandler(initParameters, operation);
                case OperationType.Commission:
                    return CommissionHandler(initParameters, operation);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private PortfolioParameters AccountActionHandler(PortfolioParameters parameters, CurrencyOperation operation, 
            Func<decimal, decimal, decimal> sumFunc)
        {
            var withNewBalance = BalanceHandler(parameters, operation, sumFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            return withNewBalance with
            {
                InvestedSum = sumFunc(parameters.InvestedSum, rubTotal),
                Cost = sumFunc(parameters.Cost, rubTotal)
            };
        }

        private PortfolioParameters PaymentActionHandler(PortfolioParameters parameters, CurrencyOperation operation)
        {
            var withNewBalance = BalanceHandler(parameters, operation, PlusFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            var newDividendProfit = parameters.DividendProfit + rubTotal;
            return withNewBalance with
            {
                Cost = parameters.Cost + rubTotal,
                DividendProfit = newDividendProfit,
                DividendProfitPercent = newDividendProfit / parameters.InvestedSum,
            };
        }

        private PortfolioParameters CommissionHandler(PortfolioParameters parameters, CurrencyOperation operation)
        {
            var withNewBalance = BalanceHandler(parameters, operation, MinusFunc);
            var rubTotal = operation.Total / operation.Currency.RubRate;
            
            return withNewBalance with
            {
                Cost = parameters.Cost - rubTotal,
            };
        }

        private PortfolioParameters BalanceHandler(PortfolioParameters parameters, CurrencyOperation operation,
            Func<decimal, decimal, decimal> sumFunc)
        {
            if (operation.Currency.Ticket == SeedFinanceData.RUB_TICKET)
            {
                return parameters with {RubBalance = sumFunc(parameters.RubBalance, operation.Total)};
            }

            if (operation.Currency.Ticket == SeedFinanceData.EURO_TICKET)
            {
                return parameters with {RubBalance = sumFunc(parameters.EuroBalance, operation.Total)};
            }

            if (operation.Currency.Ticket == SeedFinanceData.DOLLAR_TICKET)
            {
                return parameters with {DollarBalance = sumFunc(parameters.DollarBalance, operation.Total)};
            }

            return parameters;
        }
    }
}