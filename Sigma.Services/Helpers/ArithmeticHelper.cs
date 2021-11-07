using System;

namespace Sigma.Services.Helpers
{
    public static class ArithmeticHelper
    {
        public static readonly Func<decimal, decimal, decimal> SafeDivFunc = (a, b) => b != 0 ? a / b : 0;
    }
}