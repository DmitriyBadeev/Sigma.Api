using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sigma.Integrations.Moex.Models.Candles;
using Sigma.Services.Extensions;
using Sigma.Services.Services;

namespace Sigma.Services.Test
{
    [TestFixture]
    public class AnalyticServiceTest
    {
        [Test]
        public void GetAverageProfit()
        {
            var analyticService = new AnalyticService();

            var pricesCandles = new List<Candle>
            {
                new() { Open = 100, Close = 120 },
                new() { Open = 100, Close = 130 },
                new() { Open = 100, Close = 125 }
            };

            var averageProfitActual = analyticService.GetAverageProfit(pricesCandles);
            var averageProfitExpect = new List<decimal> { 20, 30, 25 }.Average();
            
            Assert.AreEqual(averageProfitExpect, averageProfitActual);
        }
        
        [Test]
        public void GetRisk()
        {
            var analyticService = new AnalyticService();

            var pricesCandles = new List<Candle>
            {
                new() { Open = 100, Close = 120 },
                new() { Open = 100, Close = 130 },
                new() { Open = 100, Close = 125 }
            };

            var riskActual = analyticService.GetRisk(pricesCandles);
            var riskExpect = new List<decimal> { 20, 30, 25 }.StandardDeviation();
            
            Assert.AreEqual(riskExpect, riskActual);
        }
    }
}