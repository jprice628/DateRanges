using System;

namespace DateRanges
{
    /// <summary>
    /// Provides a set of extension methods for the DateTime structure.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Converts a DateTime value to a Date value.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>A DateTime value with its time components set to zero and 
        /// its 'Kind' set to 'Unspecified'.</returns>
        public static DateTime ToDate(this DateTime value)
        {
            return Date.NewDate(value);
        }

        /// <summary>
        /// Converts a DateTime value to a string in the format 'yyyy-MM-dd'.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>A string representation of the DateTime value.</returns>
        public static string ToYMDString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }
    }
}
