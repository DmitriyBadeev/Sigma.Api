using System;
using NUnit.Framework;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;
using Sigma.Services.Services.SynchronizationService;
using Sigma.Services.Test.Mocks;

namespace Sigma.Services.Test
{
    [TestFixture]
    public class AssetOperationHandlerTest
    {
        [Test]
        public void BuyStockOperation_Common()
        {
            var marketDataProviderMock = new MockMarketDataProvider();
            var assetOperationHandler = new AssetOperationHandler(marketDataProviderMock);
            var initParameters = new PortfolioParameters
            {
                Cost = 10000,
                InvestedSum = 10000,
                RubBalance = 10000
            };
            
            var operations = new[]
            {
                new AssetOperation
                {
                    Ticket = "YNDX",
                    Amount = 3,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2500,
                    Total = 2500 * 3,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today.AddDays(3)
                },
                new AssetOperation
                {
                    Ticket = "YNDX",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2000,
                    Total = 2000 * 2,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today.AddDays(2)
                },
                new AssetOperation
                {
                    Ticket = "SBER",
                    Amount = 10,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 200,
                    Total = 200 * 10,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today
                },
            };

            var resultPortfolioStocks = new[]
            {
                new PortfolioStock
                {
                    Amount = 5,
                    BoughtPrice = 11500,
                    Cost = 15000,
                    PaperProfit = 3500,
                    PaperProfitPercent = (decimal)3500 / 11500
                },
                new PortfolioStock
                {
                    Amount = 10,
                    BoughtPrice = 2000,
                    Cost = 2000,
                    PaperProfit = 0,
                    PaperProfitPercent = 0
                }
            };

            var resultPortfolioParams = new PortfolioParameters
            {
                Cost = 17000 - 3500,
                RubBalance = -13500 + 10000,
                Profit = 3500,
                ProfitPercent = (decimal) 0.35,
                InvestedSum = 10000,
            };

            var actual = assetOperationHandler.Handle(initParameters, operations);

            PortfolioParametersEquals(resultPortfolioParams, actual);
            
            Assert.AreEqual(2, actual.Stocks.Count);
            Assert.AreEqual(0, actual.Fonds.Count);
            Assert.AreEqual(0, actual.Bonds.Count);

            AssetEquals(resultPortfolioStocks[0], actual.Stocks[0]);
            AssetEquals(resultPortfolioStocks[1], actual.Stocks[1]);
        }
        
        [Test]
        public void BuyAndSellFondOperations_Common()
        {
            var marketDataProviderMock = new MockMarketDataProvider();
            var assetOperationHandler = new AssetOperationHandler(marketDataProviderMock);
            var initParameters = new PortfolioParameters
            {
                Cost = 10000,
                InvestedSum = 10000,
                RubBalance = 10000
            };
            
            var operations = new[]
            {
                new AssetOperation
                {
                    Ticket = "FXGD",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2500,
                    Total = 2500 * 2,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Fond,
                    Date = DateTime.Today.AddDays(3)
                },
                new AssetOperation
                {
                    Ticket = "FXGD",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 3000,
                    Total = 3000 * 2,
                    AssetAction = AssetAction.SellAction,
                    AssetType = AssetType.Fond,
                    Date = DateTime.Today.AddDays(2)
                }
            };

            var resultPortfolioAssets = new []
            {
                new PortfolioFond
                {
                    Amount = 0,
                    Cost = 0,
                    BoughtPrice = -1000,
                    PaperProfit = 1000,
                    PaperProfitPercent = -1,
                }
            };

            var resultPortfolioParams = new PortfolioParameters
            {
                Cost = 1000 + 10000,
                RubBalance = 1000 + 10000,
                Profit = 1000,
                ProfitPercent = (decimal) 0.1,
                InvestedSum = 10000,
            };

            var actual = assetOperationHandler.Handle(initParameters, operations);

            PortfolioParametersEquals(resultPortfolioParams, actual);
            
            Assert.AreEqual(0, actual.Stocks.Count);
            Assert.AreEqual(1, actual.Fonds.Count);
            Assert.AreEqual(0, actual.Bonds.Count);

            AssetEquals(resultPortfolioAssets[0], actual.Fonds[0]);
        }

        [Test]
        public void BuyAndSellBondOperations_Common()
        {
            var marketDataProviderMock = new MockMarketDataProvider();
            var assetOperationHandler = new AssetOperationHandler(marketDataProviderMock);
            var initParameters = new PortfolioParameters
            {
                Cost = 10000,
                InvestedSum = 10000,
                RubBalance = 10000
            };
            
            var operations = new[]
            {
                new AssetOperation
                {
                    Ticket = "SU000",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2500,
                    Total = 2500 * 2,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Bond,
                    Date = DateTime.Today.AddDays(3)
                },
                new AssetOperation
                {
                    Ticket = "SU000",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 3000,
                    Total = 3000 * 2,
                    AssetAction = AssetAction.SellAction,
                    AssetType = AssetType.Bond,
                    Date = DateTime.Today.AddDays(2)
                }
            };

            var resultPortfolioAssets = new []
            {
                new PortfolioBond
                {
                    Amount = 0,
                    Cost = 0,
                    BoughtPrice = -1000,
                    PaperProfit = 1000,
                    PaperProfitPercent = -1,
                }
            };

            var resultPortfolioParams = new PortfolioParameters
            {
                Cost = 1000 + 10000,
                RubBalance = 1000 + 10000,
                Profit = 1000,
                ProfitPercent = (decimal) 0.1,
                InvestedSum = 10000,
            };

            var actual = assetOperationHandler.Handle(initParameters, operations);

            PortfolioParametersEquals(resultPortfolioParams, actual);
            
            Assert.AreEqual(0, actual.Stocks.Count);
            Assert.AreEqual(0, actual.Fonds.Count);
            Assert.AreEqual(1, actual.Bonds.Count);

            AssetEquals(resultPortfolioAssets[0], actual.Bonds[0]);
        }
        
        [Test]
        public void DualUsage_Common()
        {
            var marketDataProviderMock = new MockMarketDataProvider();
            var assetOperationHandler = new AssetOperationHandler(marketDataProviderMock);
            var initParameters = new PortfolioParameters
            {
                Cost = 10000,
                InvestedSum = 10000,
                RubBalance = 10000
            };
            
            var operations = new[]
            {
                new AssetOperation
                {
                    Ticket = "YNDX",
                    Amount = 3,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2500,
                    Total = 2500 * 3,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today.AddDays(3)
                },
                new AssetOperation
                {
                    Ticket = "YNDX",
                    Amount = 2,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 2000,
                    Total = 2000 * 2,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today.AddDays(2)
                },
                new AssetOperation
                {
                    Ticket = "SBER",
                    Amount = 10,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 200,
                    Total = 200 * 10,
                    AssetAction = AssetAction.BuyAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today
                },
            };

            var firstUsageParams = assetOperationHandler.Handle(initParameters, operations);
            
            var secondOperations = new[]
            {
                new AssetOperation
                {
                    Ticket = "SBER",
                    Amount = 10,
                    Currency = MockCurrencies.RubCurrency,
                    Price = 200,
                    Total = 200 * 10,
                    AssetAction = AssetAction.SellAction,
                    AssetType = AssetType.Stock,
                    Date = DateTime.Today
                },
            };
            
            var secondUsageParams = assetOperationHandler.Handle(firstUsageParams, secondOperations);
            var actual = secondUsageParams;
            
            var resultPortfolioParams = new PortfolioParameters
            {
                Cost = 15000 - 1500,
                RubBalance = -13500 + 10000 + 2000,
                Profit = 3500,
                ProfitPercent = (decimal) 0.35,
                InvestedSum = 10000,
            };
            
            var resultPortfolioStocks = new[]
            {
                new PortfolioStock
                {
                    Amount = 5,
                    BoughtPrice = 11500,
                    Cost = 15000,
                    PaperProfit = 3500,
                    PaperProfitPercent = (decimal)3500 / 11500
                },
                new PortfolioStock
                {
                    Amount = 0,
                    BoughtPrice = 0,
                    Cost = 0,
                    PaperProfit = 0,
                    PaperProfitPercent = 0
                }
            };
            
            PortfolioParametersEquals(resultPortfolioParams, actual);
            
            Assert.AreEqual(2, actual.Stocks.Count);
            Assert.AreEqual(0, actual.Fonds.Count);
            Assert.AreEqual(0, actual.Bonds.Count);

            AssetEquals(resultPortfolioStocks[0], actual.Stocks[0]);
            AssetEquals(resultPortfolioStocks[1], actual.Stocks[1]);
        }

        private void PortfolioParametersEquals(PortfolioParameters portfolioParametersExpected,
            PortfolioParameters portfolioParametersActual)
        {
            Assert.AreEqual(portfolioParametersExpected.Cost,  portfolioParametersActual.Cost);
            Assert.AreEqual(portfolioParametersExpected.InvestedSum, portfolioParametersActual.InvestedSum);
            Assert.AreEqual(portfolioParametersExpected.RubBalance, portfolioParametersActual.RubBalance);
            Assert.AreEqual(portfolioParametersExpected.EuroBalance, portfolioParametersActual.EuroBalance);
            Assert.AreEqual(portfolioParametersExpected.DollarBalance, portfolioParametersActual.DollarBalance);
            Assert.AreEqual(portfolioParametersExpected.DividendProfit, portfolioParametersActual.DividendProfit);
            Assert.AreEqual(portfolioParametersExpected.DividendProfitPercent, portfolioParametersActual.DividendProfitPercent);
            Assert.AreEqual(portfolioParametersExpected.Profit, portfolioParametersActual.Profit);
            Assert.AreEqual(portfolioParametersExpected.ProfitPercent, portfolioParametersActual.ProfitPercent);
        }
        
        private void AssetEquals(IPortfolioAsset portfolioStockExpected, IPortfolioAsset portfolioStockActual)
        {
            Assert.AreEqual(portfolioStockExpected.Amount, portfolioStockActual.Amount);
            Assert.AreEqual(portfolioStockExpected.BoughtPrice, portfolioStockActual.BoughtPrice);
            Assert.AreEqual(portfolioStockExpected.Cost, portfolioStockActual.Cost);
            Assert.AreEqual(portfolioStockExpected.PaperProfit, portfolioStockActual.PaperProfit);
            Assert.AreEqual(portfolioStockExpected.PaperProfitPercent, portfolioStockActual.PaperProfitPercent);
        }
    }
}