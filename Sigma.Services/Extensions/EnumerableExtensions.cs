using System;
using System.Collections.Generic;
using System.Linq;

namespace Sigma.Services.Extensions
{
    public static class EnumerableExtensions
    {
        public static decimal StandardDeviation(this IEnumerable<decimal> source)
        {
            var sourceList = source.ToList();
            var average = sourceList.Average();
            var sumOfSquaresOfDifferences = sourceList
                .Select(val => (val - average) * (val - average))
                .Sum();

            if (sumOfSquaresOfDifferences == 0)
            {
                return 0;
            }
            
            return (decimal) Math.Sqrt((double) sumOfSquaresOfDifferences / sourceList.Count);
        }
        
        public static IEnumerable<decimal> ToAbsolute(this IEnumerable<decimal> source)
        {
            return source.Select(Math.Abs);
        }
    }
}