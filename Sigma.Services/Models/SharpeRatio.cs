using Sigma.Services.Helpers;

namespace Sigma.Services.Models
{
    public class SharpeRatio
    {
        public SharpeRatio(decimal risk, decimal safeRate, decimal profit)
        {
            Risk = risk;
            SafeRate = safeRate;
            Profit = profit;
        }

        public decimal Value => ArithmeticHelper.SafeDivFunc(Profit - SafeRate, Risk);
        public decimal Risk { get; }
        public decimal Profit { get; }
        public decimal SafeRate { get; }

        public SharpRatioInterpretation Interpretation => Value switch
        {
            < (decimal) 0.3 => SharpRatioInterpretation.Terrible,
            < (decimal) 0.6 => SharpRatioInterpretation.Bad,
            < (decimal) 0.8 => SharpRatioInterpretation.Normal,
            < (decimal) 1.0 => SharpRatioInterpretation.Good,
            _ => SharpRatioInterpretation.Excellent
        };
    }

    public enum SharpRatioInterpretation
    {
        Excellent = 0,
        Good = 1,
        Normal = 2,
        Bad = 3,
        Terrible = 4
    }
}