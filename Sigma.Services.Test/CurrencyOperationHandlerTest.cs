using System;
using NUnit.Framework;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Services.Services.SynchronizationService;
using Sigma.Services.Test.Mocks;

namespace Sigma.Services.Test
{
    [TestFixture]
    public class CurrencyOperationHandlerTest
    {
        [Test]
        public void RefillActionTest_Common()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var portfolioParameters = new PortfolioParameters();

            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 400,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                }                
            };

            var result = currencyHandler.Handle(portfolioParameters, operations);

            Assert.AreEqual(result.Cost,  500);
            Assert.AreEqual(result.InvestedSum,  500);
            Assert.AreEqual(result.RubBalance,  500);
            Assert.AreEqual(result.EuroBalance,  0);
            Assert.AreEqual(result.DollarBalance,  0);
            Assert.AreEqual(result.DividendProfit,  0);
            Assert.AreEqual(result.DividendProfitPercent,  0);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }

        [Test]
        public void WithdrawalAction_Common()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();

            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 400,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 300,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                }
            };

            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  150);
            Assert.AreEqual(result.InvestedSum,  150);
            Assert.AreEqual(result.RubBalance,  150);
            Assert.AreEqual(result.EuroBalance,  0);
            Assert.AreEqual(result.DollarBalance,  0);
            Assert.AreEqual(result.DividendProfit,  0);
            Assert.AreEqual(result.DividendProfitPercent,  0);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }

        [Test]
        public void PaymentsAction_Common()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();

            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.CouponPayment,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 500,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                }
            };

            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  550);
            Assert.AreEqual(result.InvestedSum,  400);
            Assert.AreEqual(result.RubBalance,  550);
            Assert.AreEqual(result.EuroBalance,  0);
            Assert.AreEqual(result.DollarBalance,  0);
            Assert.AreEqual(result.DividendProfit,  150);
            Assert.AreEqual(result.DividendProfitPercent,  0.375);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }

        [Test]
        public void CommissionAction_Common()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();
            
            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.Commission,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 500,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.Commission,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                }
            };

            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  450);
            Assert.AreEqual(result.InvestedSum,  500);
            Assert.AreEqual(result.RubBalance,  450);
            Assert.AreEqual(result.EuroBalance,  0);
            Assert.AreEqual(result.DollarBalance,  0);
            Assert.AreEqual(result.DividendProfit,  100);
            Assert.AreEqual(result.DividendProfitPercent,  0.2);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }
        
        [Test]
        public void AccountAction_WithCurrencies()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();

            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 1000,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 200,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.EuroCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 200,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 20,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.EuroCurrency,
                    Date = DateTime.Now
                },
            };
            
            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  23800);
            Assert.AreEqual(result.InvestedSum,  23800);
            Assert.AreEqual(result.RubBalance,  800);
            Assert.AreEqual(result.EuroBalance,  100);
            Assert.AreEqual(result.DollarBalance,  30);
            Assert.AreEqual(result.DividendProfit,  0);
            Assert.AreEqual(result.DividendProfitPercent,  0);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }

        [Test]
        public void PaymentsAction_WithCurrencies()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();

            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 1000,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.EuroCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 10000,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 20,
                    OperationType = OperationType.CouponPayment,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.WithdrawalAction,
                    Currency = MockCurrencies.EuroCurrency,
                    Date = DateTime.Now
                },
            };
            
            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  23000);
            Assert.AreEqual(result.InvestedSum,  -5000);
            Assert.AreEqual(result.RubBalance,  11000);
            Assert.AreEqual(result.EuroBalance,  50);
            Assert.AreEqual(result.DollarBalance,  20);
            Assert.AreEqual(result.DividendProfit,  28000);
            Assert.AreEqual(result.DividendProfitPercent,  5.6);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }

        [Test]
        public void CommissionAction_WithCurrencies()
        {
            var currencyHandler = new CurrencyOperationHandler();
            var initParameters = new PortfolioParameters();
            
            var operations = new[]
            {
                new CurrencyOperation
                {
                    Total = 100,
                    OperationType = OperationType.DividendPayment,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 50,
                    OperationType = OperationType.Commission,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 1000,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 1,
                    OperationType = OperationType.Commission,
                    Currency = MockCurrencies.DollarCurrency,
                    Date = DateTime.Now
                },
                new CurrencyOperation
                {
                    Total = 1,
                    OperationType = OperationType.Commission,
                    Currency = MockCurrencies.EuroCurrency,
                    Date = DateTime.Now
                }
            };

            var result = currencyHandler.Handle(initParameters, operations);
            
            Assert.AreEqual(result.Cost,  750);
            Assert.AreEqual(result.InvestedSum,  1000);
            Assert.AreEqual(result.RubBalance,  1050);
            Assert.AreEqual(result.EuroBalance,  -1);
            Assert.AreEqual(result.DollarBalance,  -1);
            Assert.AreEqual(result.DividendProfit,  100);
            Assert.AreEqual(result.DividendProfitPercent,  0.1);
            Assert.AreEqual(result.Profit,  0);
            Assert.AreEqual(result.ProfitPercent,  0);
        }
    }
}