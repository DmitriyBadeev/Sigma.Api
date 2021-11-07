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
    public class IntegrationOperationsTest
    {
        [Test]
        public void RefillAndBuyStock()
        {
            var marketDataProviderMock = new MockMarketDataProvider();
            var assetOperationHandler = new AssetOperationHandler(marketDataProviderMock);
            var currencyOperationHandler = new CurrencyOperationHandler();

            var initParameters = new PortfolioParameters();
                
            var currencyOperations = new[]
            {
                new CurrencyOperation
                {
                    Total = 10000,
                    OperationType = OperationType.RefillAction,
                    Currency = MockCurrencies.RubCurrency,
                    Date = DateTime.Today
                },
            };

            var assetOperations = new[]
            {
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
                }
            };
            
            var resultPortfolioStocks = new[]
            {
                new PortfolioStock
                {
                    Amount = 10,
                    BoughtPrice = 2000,
                    Cost = 2000,
                    PaperProfit = 0,
                    PaperProfitPercent = 0
                },
            };
            
            var resultPortfolioParams = new PortfolioParameters
            {
                Cost = 10000,
                RubBalance = 10000 - 2000,
                Profit = 0,
                ProfitPercent = 0,
                InvestedSum = 10000,
            };

            var resultWithCurrencyOperations = currencyOperationHandler.Handle(initParameters, currencyOperations);
            var actual = assetOperationHandler.Handle(resultWithCurrencyOperations, assetOperations);
            
            PortfolioParametersEquals(resultPortfolioParams, actual);
            
            Assert.AreEqual(1, actual.Stocks.Count);
            Assert.AreEqual(0, actual.Fonds.Count);
            Assert.AreEqual(0, actual.Bonds.Count);

            AssetEquals(resultPortfolioStocks[0], actual.Stocks[0]);
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