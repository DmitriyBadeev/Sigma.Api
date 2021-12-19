using System;

namespace Sigma.Services.Extensions
{
    public static class DateTimeExtensions
    {
        public static long MillisecondsTimestamp(this DateTime date)
        {
            var baseDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(date.ToUniversalTime()-baseDate).TotalMilliseconds;
        }

        public static bool IsFuture(this DateTime date)
        {
            return DateTime.Compare(DateTime.Today, date) <= 0;
        }
    }
}