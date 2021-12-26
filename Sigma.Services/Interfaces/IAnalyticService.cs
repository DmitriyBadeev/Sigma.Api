﻿using System.Collections.Generic;
using Sigma.Core.Entities;
using Sigma.Integrations.Moex.Models.Candles;
using Sigma.Services.Models;

namespace Sigma.Services.Interfaces
{
    public interface IAnalyticService
    {
        HerfindahlHirschmanIndex GetHerfindahlHirschmanIndex(Portfolio portfolio);
        HerfindahlHirschmanIndexInterpretation GetHerfindahlHirschmanInterpretation(decimal value);
        List<AssetShare> GetPortfolioAssetShares(Portfolio portfolio);
        decimal GetAverageProfit(List<Candle> priceCandles);
        decimal GetRisk(List<Candle> priceCandles);
        decimal GetSharpeRatio(decimal profit, decimal risk);
    }
}