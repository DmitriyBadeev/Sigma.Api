using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Services.Interfaces;
using Sigma.Services.Models;

namespace Sigma.Services.Services
{
    public class AnalyticService : IAnalyticService
    {
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
            var shares = new List<AssetShare>();

            foreach (var stock in portfolio.PortfolioStocks)
            {
                var percent = (stock.Cost / paperCost) * 100;
                var share = new AssetShare(stock.Stock.Ticket, stock.Stock.ShortName, percent);
                shares.Add(share);
            }
            
            foreach (var fond in portfolio.PortfolioFonds)
            {
                var percent = (fond.Cost / paperCost) * 100;
                var share = new AssetShare(fond.Fond.Ticket, fond.Fond.ShortName, percent);
                shares.Add(share);
            }
            
            foreach (var bond in portfolio.PortfolioBonds)
            {
                var percent = (bond.Cost / paperCost) * 100;
                var share = new AssetShare(bond.Bond.Ticket, bond.Bond.ShortName, percent);
                shares.Add(share);
            }

            return shares;
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
    }
}