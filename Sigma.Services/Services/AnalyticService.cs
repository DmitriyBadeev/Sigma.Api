using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.Models.Candles;
using Sigma.Services.Extensions;
using Sigma.Services.Helpers;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Services.Services
{
    public class AnalyticService : IAnalyticService
    {
        private readonly decimal _safeRate = new(0.65);
        
        public HerfindahlHirschmanIndex GetHerfindahlHirschmanIndex(Portfolio portfolio)
        {
            var paperCost = GetPaperCost(portfolio);
            var index = (decimal) 0;
            
            index += portfolio.PortfolioStocks.Sum(stock => Squared(stock.Cost / paperCost));
            index += portfolio.PortfolioFonds.Sum(fond => Squared(fond.Cost / paperCost));
            index += portfolio.PortfolioBonds.Sum(bond => Squared(bond.Cost / paperCost));

            var interpretation = GetHerfindahlHirschmanInterpretation(index);
            return new HerfindahlHirschmanIndex(index, interpretation);
        }
        
        public HerfindahlHirschmanIndexInterpretation GetHerfindahlHirschmanInterpretation(decimal value)
        {
            return value switch
            {
                < (decimal) 0.1 => HerfindahlHirschmanIndexInterpretation.Excellent,
                < (decimal) 0.15 => HerfindahlHirschmanIndexInterpretation.Good,
                < (decimal) 0.25 => HerfindahlHirschmanIndexInterpretation.Normal,
                < (decimal) 0.6 => HerfindahlHirschmanIndexInterpretation.Bad,
                _ => HerfindahlHirschmanIndexInterpretation.Terrible
            };
        }

        public List<AssetShare> GetPortfolioAssetShares(Portfolio portfolio)
        {
            var paperCost = GetPaperCost(portfolio);
            var allRisk = GetAllRisk(portfolio, paperCost);
            var shares = new List<AssetShare>();

            foreach (var stock in portfolio.PortfolioStocks)
            {
                var percent = GetAssetPercent(stock.Cost, paperCost);
                var riskPercent = GetRiskPercent(stock.Stock.Risk, stock.Cost, allRisk, paperCost);

                var assetShare = new AssetShare(stock.Stock.Ticket, stock.Stock.ShortName, percent, riskPercent);
                shares.Add(assetShare);
            }
            
            foreach (var fond in portfolio.PortfolioFonds)
            {
                var percent = GetAssetPercent(fond.Cost, paperCost);
                var riskPercent = GetRiskPercent(fond.Fond.Risk, fond.Cost, allRisk, paperCost);
                
                var assetShare = new AssetShare(fond.Fond.Ticket, fond.Fond.ShortName, percent, riskPercent);
                shares.Add(assetShare);
            }
            
            foreach (var bond in portfolio.PortfolioBonds)
            {
                var percent = GetAssetPercent(bond.Cost, paperCost);
                var riskPercent = GetRiskPercent(bond.Bond.Risk, bond.Cost, allRisk, paperCost);
                
                var assetShare = new AssetShare(bond.Bond.Ticket, bond.Bond.ShortName, percent, riskPercent);
                shares.Add(assetShare);
            }

            return shares;
        }

        private decimal GetRiskPercent(decimal assetRisk, decimal assetCost, decimal allRisk, decimal paperCost) =>
            ((assetRisk * (assetCost / paperCost)) / allRisk) * 100;
        
        private decimal GetAssetPercent(decimal assetCost, decimal paperCost) => (assetCost / paperCost) * 100;

        public SharpeRatio GetSharpeRatio(Portfolio portfolio)
        {
            var portfolioRisk = GetRisk(portfolio);
            var portfolioAverageProfit = GetAverageProfit(portfolio);

            return new SharpeRatio(portfolioRisk, _safeRate, portfolioAverageProfit);
        }

        public decimal GetAverageProfit(List<Candle> priceCandles)
        {
            var profitPercents = GetProfitPercents(priceCandles).ToList();

            return profitPercents.Count > 0 ? profitPercents.ToAbsolute().Average() : 0;
        }

        public decimal GetAverageProfit(Portfolio portfolio)
        {
            var shares = GetPortfolioAssetShares(portfolio);
            
            var stocks = portfolio.PortfolioStocks.ToList();
            var fonds = portfolio.PortfolioFonds.ToList();
            var bonds = portfolio.PortfolioBonds.ToList();

            var portfolioAverageProfit = (decimal) 0;
            foreach (var stock in stocks)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == stock.Stock.Ticket)?.Percent ?? 0) / 100;
                portfolioAverageProfit += stock.Stock.AverageProfit * share;
            }
            
            foreach (var fond in fonds)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == fond.Fond.Ticket)?.Percent ?? 0) / 100;
                portfolioAverageProfit += fond.Fond.AverageProfit * share;
            }
            
            foreach (var bond in bonds)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == bond.Bond.Ticket)?.Percent ?? 0) / 100;
                portfolioAverageProfit += bond.Bond.AverageProfit * share;
            }

            return portfolioAverageProfit;
        }

        public decimal GetRisk(List<Candle> priceCandles)
        {
            var profitPercents = GetProfitPercents(priceCandles).ToList();
            
            return profitPercents.Count > 0 ? profitPercents.StandardDeviation() : 0;
        }

        public decimal GetRisk(Portfolio portfolio)
        {
            var shares = GetPortfolioAssetShares(portfolio);
            
            var stocks = portfolio.PortfolioStocks.ToList();
            var fonds = portfolio.PortfolioFonds.ToList();
            var bonds = portfolio.PortfolioBonds.ToList();

            var portfolioRisk = (decimal) 0;
            foreach (var stock in stocks)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == stock.Stock.Ticket)?.Percent ?? 0) / 100;
                portfolioRisk += stock.Stock.Risk * share;
            }
            
            foreach (var fond in fonds)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == fond.Fond.Ticket)?.Percent ?? 0) / 100;
                portfolioRisk += fond.Fond.Risk * share;
            }
            
            foreach (var bond in bonds)
            {
                var share = (shares.FirstOrDefault(s => s.Ticket == bond.Bond.Ticket)?.Percent ?? 0) / 100;
                portfolioRisk += bond.Bond.Risk * share;
            }

            return portfolioRisk;
        }

        public decimal GetSharpeRatio(decimal profit, decimal risk)
        {
            return ArithmeticHelper.SafeDivFunc(profit - _safeRate, risk);
        }

        private IEnumerable<decimal> GetProfitPercents(List<Candle> priceCandles)
        {
            var profitPercents = new List<decimal>();
            foreach (var priceCandle in priceCandles)
            {
                var beginIntervalPrice = priceCandle.Open;
                var endIntervalPrice = priceCandle.Close;
                var profit = endIntervalPrice - beginIntervalPrice;
                var profitPercent = ArithmeticHelper.SafeDivFunc(profit, beginIntervalPrice) * 100;
                profitPercents.Add(profitPercent);
            }

            return profitPercents;
        }

        private decimal Squared(decimal value) => value * value;

        private decimal GetPaperCost(Portfolio portfolio)
        {
            var paperCost = (decimal)0;
            paperCost += portfolio.PortfolioStocks.Sum(stock => stock.Cost);
            paperCost += portfolio.PortfolioBonds.Sum(bond => bond.Cost);
            paperCost += portfolio.PortfolioFonds.Sum(fond => fond.Cost);

            return paperCost;
        }

        private decimal GetAllRisk(Portfolio portfolio, decimal paperCost)
        {
            var allRisk = (decimal)0;

            allRisk += portfolio.PortfolioStocks.Sum(stock => stock.Stock.Risk * ArithmeticHelper.SafeDivFunc(stock.Cost, paperCost));
            allRisk += portfolio.PortfolioFonds.Sum(fond => fond.Fond.Risk * ArithmeticHelper.SafeDivFunc(fond.Cost, paperCost));
            allRisk += portfolio.PortfolioBonds.Sum(bond => bond.Bond.Risk * ArithmeticHelper.SafeDivFunc(bond.Cost, paperCost));

            return allRisk;
        }
    }
}