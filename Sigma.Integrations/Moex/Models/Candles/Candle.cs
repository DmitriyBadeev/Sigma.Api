using System;
using Sigma.Core.Interfaces;

namespace Sigma.Integrations.Moex.Models.Candles
{
    public class Candle : IRequested
    {
        public decimal Open { get; init; }
        public decimal Close { get; init; }
        public decimal High { get; init; }
        public decimal Low { get; init; }
        public decimal Value { get; init; }
        public decimal Volume { get; init; }
        public DateTime Date { get; init; }
    }

    public enum CandleInterval
    {
        Week = 7,
        Day = 24,
        Month = 31,
        Hour = 60
    }
}