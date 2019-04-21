using System;

namespace DateRanges
{
    /// <summary>
    /// Provides a set of functions for date values.
    /// </summary>
    public static class Date
    {
        /// <summary>
        /// Gets the current date with its time components set to zero and 
        /// its 'Kind' set to 'Unspecified'.
        /// </summary>
        public static DateTime Today { get => NewDate(DateTime.Today); }

        /// <summary>
        /// Gets the maximum date with its time components set to zero and 
        /// its 'Kind' set to 'Unspecified'.
        /// </summary>
        public static DateTime MaxValue { get => NewDate(DateTime.MaxValue); }

        /// <summary>
        /// Gets the minimum date with its time components set to zero and 
        /// its 'Kind' set to 'Unspecified'.
        /// </summary>
        public static DateTime MinValue { get => NewDate(DateTime.MinValue); }

        /// <summary>
        /// Initializes a new date value with the specified year, month, and 
        /// day. The value's time components are set to zero and its 'Kind' is 
        /// set to 'Unspecified'.
        /// </summary>
        public static DateTime NewDate(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Initializes a new date value using the year, month, and day values 
        /// from the provided DateTime value. Time components are set to zero 
        /// and 'Kind' is set to 'Unspecified'.
        /// </summary>
        public static DateTime NewDate(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Determines whether or not the provided DateTime value is a date.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>True if the value's time components are set to zero and 
        /// 'Kind' is set to 'Unspecified'; otherwise, false.</returns>
        public static bool IsDate(DateTime value)
        {
            return value.Hour == 0 &&
                value.Minute == 0 &&
                value.Second == 0 &&
                value.Kind == DateTimeKind.Unspecified;
        }

        /// <summary>
        /// Determines whether or not the provided DateTime value is equal to 
        /// the maximum date value.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>True if 'value' is equal to Date.MaxValue; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when 'value' is not a date.</exception>
        public static bool IsMaxValue(DateTime value)
        {
            if (!IsDate(value)) throw new ArgumentException("Error comparing to max value: 'value' is not a date.");
            return AreEqual(value, MaxValue);
        }

        /// <summary>
        /// Determines whether or not the provided DateTime value is equal to 
        /// the minimum date value.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>True if 'value' is equal to Date.MinValue; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when 'value' is not a date.</exception>
        public static bool IsMinValue(DateTime value)
        {
            if (!IsDate(value)) throw new ArgumentException("Error comparing to min value: 'value' is not a date.");
            return AreEqual(value, MinValue);
        }

        /// <summary>
        /// Determines whether or not the provided DateTime value is equal to 
        /// today's date.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>True if 'value' is equal to Date.Today; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when 'value' is not a date.</exception>
        public static bool IsToday(DateTime value)
        {
            if (!IsDate(value)) throw new ArgumentException("Error testing for today's date: 'value' is not a date.");
            return AreEqual(value, Today);
        }

        /// <summary>
        /// Determines whether or not two dates are equal.
        /// </summary>
        /// <param name="a">A DateTime value.</param>
        /// <param name="b">A DateTime value.</param>
        /// <returns>True if 'a' and 'b' are equal; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when either 'a' or 'b' is not a date.</exception>
        public static bool AreEqual(DateTime a, DateTime b)
        {
            if (!IsDate(a)) throw new ArgumentException("Error testing date equality: 'a' is not a date.");
            if (!IsDate(b)) throw new ArgumentException("Error testing date equality: 'b' is not a date.");

            return a == b;
        }

        /// <summary>
        /// Determines the lesser of two date values.
        /// </summary>
        /// <param name="a">A DateTime value.</param>
        /// <param name="b">A DateTime value.</param>
        /// <returns>The lesser of 'a' and 'b'.</returns>
        /// <exception cref="ArgumentException">Thrown when either 'a' or 'b' is not a date.</exception>
        public static DateTime Min(DateTime a, DateTime b)
        {
            if (!IsDate(a)) throw new ArgumentException("Error testing for min value: 'a' is not a date.");
            if (!IsDate(b)) throw new ArgumentException("Error testing for min value: 'b' is not a date.");

            return a < b ? a : b;
        }

        /// <summary>
        /// Determines the greater of two date values.
        /// </summary>
        /// <param name="a">A DateTime value.</param>
        /// <param name="b">A DateTime value.</param>
        /// <returns>The greater of 'a' and 'b'.</returns>
        /// <exception cref="ArgumentException">Thrown when either 'a' or 'b' 
        /// is not a date.</exception>
        public static DateTime Max(DateTime a, DateTime b)
        {
            if (!IsDate(a)) throw new ArgumentException("Error testing for max value: 'a' is not a date.");
            if (!IsDate(b)) throw new ArgumentException("Error testing for max value: 'b' is not a date.");

            return a > b ? a : b;
        }

        /// <summary>
        /// Clamps the specified date between the specified min and max date values.
        /// </summary>
        /// <param name="date">A DateTime value to be clamped.</param>
        /// <param name="minDate">A DateTime value which specifies the lower 
        /// boundary of the result.</param>
        /// /// <param name="maxDate">A DateTime value which specifies the 
        /// upper boundary of the result.</param>
        /// <returns>'minDate' when 'date' is less than 'minDate', 'maxDate' 
        /// when 'date' is greater than 'maxDate'; otherwise, 'date'.</returns>
        /// <exception cref="ArgumentException">Thrown when either 'date', 
        /// 'minDate', or 'maxDate' is not a date.</exception>
        public static DateTime Clamp(DateTime date, DateTime minDate, DateTime maxDate)
        {
            if (!IsDate(date)) throw new ArgumentException("Error clamping date: 'date' is not a date.");
            if (!IsDate(minDate)) throw new ArgumentException("Error clamping date: 'minDate' is not a date.");
            if (!IsDate(maxDate)) throw new ArgumentException("Error clamping date: 'maxDate' is not a date.");

            if (date < minDate)
            {
                return minDate;
            }
            else if (date > maxDate)
            {
                return maxDate;
            }
            else
            {
                return date;
            }
        }
    }
}
