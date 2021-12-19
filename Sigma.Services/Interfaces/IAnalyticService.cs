using System.Collections.Generic;
using Sigma.Core.Entities;
using Sigma.Services.Models;

namespace Sigma.Services.Interfaces
{
    public interface IAnalyticService
    {
        HerfindahlHirschmanIndex GetHerfindahlHirschmanIndex(Portfolio portfolio);
        HerfindahlHirschmanIndexInterpretation GetHerfindahlHirschmanInterpretation(decimal value);

        List<AssetShare> GetPortfolioAssetShares(Portfolio portfolio);
    }
}