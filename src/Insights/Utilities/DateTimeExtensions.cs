using System;

namespace Insights.Utilities
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Truncates a DateTime to the specified resolution.
        /// </summary>
        /// <param name="date">Specifies the DateTime value to truncate.</param>
        /// <param name="resolution">Specifies the resolution for rounding like hour, minute, or ssecond. Use TimeSpan.TicksPer* values.</param>
        /// <returns>Truncated DateTime</returns>
        /// <remarks>
        /// Use TimeSpan.TicksPerXXXX to supply the resolution value.
        /// 
        /// Source:
        /// 
        /// https://stackoverflow.com/questions/1004698/how-to-truncate-milliseconds-off-of-a-net-datetime
        /// </remarks>
        public static DateTime Truncate(this DateTime date, long resolution)
        {
            return new DateTime(date.Ticks - (date.Ticks % resolution), date.Kind);
        }
    }
}
