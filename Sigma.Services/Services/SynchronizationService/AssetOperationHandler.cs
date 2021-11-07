using System;
using System.Collections.Generic;
using System.Linq;
using Sigma.Core.Entities;
using Sigma.Core.Enums;
using Sigma.Core.Interfaces;
using Sigma.Services.Interfaces;

namespace Sigma.Services.Services.SynchronizationService
{
    public class AssetOperationHandler
    {
        private readonly IMarketDataProvider _marketDataProvider;
        private static readonly Func<decimal, decimal, decimal> SafeDivFunc = (a, b) => b != 0 ? a / b : 0;
        public AssetOperationHandler(IMarketDataProvider marketDataProvider)
        {
            _marketDataProvider = marketDataProvider;
        }

        public PortfolioParameters Handle(PortfolioParameters initParameters, IEnumerable<AssetOperation> operations)
        {
            var parametersWithoutNulls = InitAssetListsIfNull(initParameters);
            var parametersWithResetLists = ResetParametersByAssets(parametersWithoutNulls);
            var parametersWithUpdatedList = operations.Aggregate(parametersWithResetLists, AssetListUpdate);
            return UpdateParametersByAssets(parametersWithUpdatedList);
        }

        private PortfolioParameters AssetListUpdate(PortfolioParameters parameters, AssetOperation operation)
        {
            var portfolioAsset = GetOrCreatePortfolioAsset(parameters, operation);
            var updatedPortfolioAsset = UpdateAssetParameters(portfolioAsset, operation);

            return InsertInAssetList(parameters, operation, updatedPortfolioAsset);
        }

        private IPortfolioAsset UpdateAssetParameters(IPortfolioAsset portfolioAsset, AssetOperation operation)
        {
            switch (operation.AssetAction)
            {
                case AssetAction.BuyAction:
                    portfolioAsset.Amount += operation.Amount;
                    portfolioAsset.BoughtPrice += operation.Total;
                    break;
                case AssetAction.SellAction:
                    portfolioAsset.Amount -= operation.Amount;
                    portfolioAsset.BoughtPrice -= operation.Total;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            portfolioAsset.Cost = portfolioAsset switch
            {
                PortfolioStock portfolioStock => portfolioAsset.Amount * portfolioStock.Stock.Price,
                PortfolioFond portfolioFond => portfolioAsset.Amount * portfolioFond.Fond.Price,
                PortfolioBond portfolioBond => portfolioBond.Amount * portfolioBond.Bond.Price,
                _ => throw new ArgumentOutOfRangeException()
            };

            portfolioAsset.PaperProfit = portfolioAsset.Cost - portfolioAsset.BoughtPrice;
            portfolioAsset.PaperProfitPercent = SafeDivFunc(portfolioAsset.PaperProfit, portfolioAsset.BoughtPrice);

            return portfolioAsset;
        }

        private PortfolioParameters UpdateParametersByAssets(PortfolioParameters parameters)
        {
            var paperCosts = parameters.Cost;
            var balance = parameters.RubBalance;
            var paperProfit = parameters.Profit;
            
            foreach (var portfolioStock in parameters.Stocks)
            {
                paperCosts += portfolioStock.Cost;
                balance -= portfolioStock.BoughtPrice;
                paperProfit += portfolioStock.PaperProfit;
            }

            foreach (var portfolioFond in parameters.Fonds)
            {
                paperCosts += portfolioFond.Cost;
                balance -= portfolioFond.BoughtPrice;
                paperProfit += portfolioFond.PaperProfit;
            }
            
            foreach (var portfolioBond in parameters.Bonds)
            {
                paperCosts += portfolioBond.Cost;
                balance -= portfolioBond.BoughtPrice;
                paperProfit += portfolioBond.PaperProfit;
            }

            return parameters with
            {
                Cost = paperCosts + balance,
                Profit = paperProfit,
                ProfitPercent = SafeDivFunc(paperProfit, parameters.InvestedSum),
                RubBalance = balance
            };
        }
        
        private PortfolioParameters ResetParametersByAssets(PortfolioParameters parameters)
        {
            var paperCosts = parameters.Cost - parameters.RubBalance;
            var balance = parameters.RubBalance;
            var paperProfit = parameters.Profit;
            
            foreach (var portfolioStock in parameters.Stocks)
            {
                paperCosts -= portfolioStock.Cost;
                balance += portfolioStock.BoughtPrice;
                paperProfit -= portfolioStock.PaperProfit;
            }

            foreach (var portfolioFond in parameters.Fonds)
            {
                paperCosts -= portfolioFond.Cost;
                balance += portfolioFond.BoughtPrice;
                paperProfit -= portfolioFond.PaperProfit;
            }
            
            foreach (var portfolioBond in parameters.Bonds)
            {
                paperCosts -= portfolioBond.Cost;
                balance += portfolioBond.BoughtPrice;
                paperProfit -= portfolioBond.PaperProfit;
            }

            return parameters with
            {
                Cost = paperCosts,
                Profit = paperProfit,
                ProfitPercent = SafeDivFunc(paperProfit, parameters.InvestedSum),
                RubBalance = balance
            };
        }
        
        private PortfolioParameters InsertInAssetList(PortfolioParameters parameters, AssetOperation operation,
            IPortfolioAsset updatedPortfolioAsset)
        {
            switch (updatedPortfolioAsset)
            {
                case PortfolioStock portfolioStock:
                {
                    var copiedList = parameters.Stocks
                        .Where(s => s.Stock.Ticket != operation.Ticket)
                        .ToList();

                    copiedList.Add(portfolioStock);

                    return parameters with {Stocks = copiedList};
                }
                case PortfolioFond portfolioFond:
                {
                    var copiedList = parameters.Fonds
                        .Where(s => s.Fond.Ticket != operation.Ticket)
                        .ToList();

                    copiedList.Add(portfolioFond);

                    return parameters with {Fonds = copiedList};
                }
                case PortfolioBond portfolioBond:
                {
                    var copiedList = parameters.Bonds
                        .Where(s => s.Bond.Ticket != operation.Ticket)
                        .ToList();

                    copiedList.Add(portfolioBond);

                    return parameters with {Bonds = copiedList};
                }
            }

            return parameters;
        }

        private IPortfolioAsset GetOrCreatePortfolioAsset(PortfolioParameters parameters, AssetOperation operation)
        {
            return operation.AssetType switch
            {
                AssetType.Stock => GetOrCreatePortfolioStock(parameters, operation.Ticket),
                AssetType.Fond => GetOrCreatePortfolioFond(parameters, operation.Ticket),
                AssetType.Bond => GetOrCreatePortfolioBond(parameters, operation.Ticket),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private PortfolioStock GetOrCreatePortfolioStock(PortfolioParameters parameters, string ticket)
        {
            var portfolioStock = parameters.Stocks.FirstOrDefault(s => s.Stock.Ticket == ticket);
            if (portfolioStock != null) return portfolioStock;
            
            var marketData = _marketDataProvider.GetAsset<Stock>(ticket);
            return new PortfolioStock
            {
                Stock = marketData
            };
        }
        
        private PortfolioFond GetOrCreatePortfolioFond(PortfolioParameters parameters, string ticket)
        {
            var portfolioFond = parameters.Fonds.FirstOrDefault(s => s.Fond.Ticket == ticket);
            if (portfolioFond != null) return portfolioFond;
            
            var marketData = _marketDataProvider.GetAsset<Fond>(ticket);
            return new PortfolioFond
            {
                Fond = marketData
            };
        }
        
        private PortfolioBond GetOrCreatePortfolioBond(PortfolioParameters parameters, string ticket)
        {
            var portfolioBond = parameters.Bonds.FirstOrDefault(s => s.Bond.Ticket == ticket);
            if (portfolioBond != null) return portfolioBond;
            
            var marketData = _marketDataProvider.GetAsset<Bond>(ticket);
            return new PortfolioBond
            {
                Bond = marketData
            };
        }

        private PortfolioParameters InitAssetListsIfNull(PortfolioParameters initParameters) =>
            initParameters with
            {
                Stocks = initParameters.Stocks ?? new List<PortfolioStock>(),
                Fonds = initParameters.Fonds ?? new List<PortfolioFond>(),
                Bonds = initParameters.Bonds ?? new List<PortfolioBond>()
            };
    }
}