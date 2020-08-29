using System;

namespace Insights.Utilities
{
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Truncates a DateTimeOffset to the specified resolution.
        /// </summary>
        /// <param name="offset">Specifies the DateTimeOffset value to truncate.</param>
        /// <param name="resolution">Specifies the resolution for rounding like hour, minute, or ssecond. Use TimeSpan.TicksPer* values.</param>
        /// <returns>Truncated DateTimeOffset</returns>
        /// <remarks>
        /// Use TimeSpan.TicksPerXXXX to supply the resolution value.
        /// 
        /// Source:
        /// 
        /// https://stackoverflow.com/questions/1004698/how-to-truncate-milliseconds-off-of-a-net-datetime
        /// </remarks>
        public static DateTimeOffset Truncate(this DateTimeOffset offset, long resolution)
        {
            return new DateTimeOffset(offset.Ticks - (offset.Ticks % resolution), offset.Offset);
        }
    }
}
