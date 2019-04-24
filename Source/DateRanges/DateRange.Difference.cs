using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Provides the difference between two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'this' DateRange 
        /// but not in the given DateRange.
        /// </returns>
        public IEnumerable<DateRange> Difference(DateRange dateRange)
        {
            return new DifferenceOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Provides the difference between a DateRange value and a set of 
        /// DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'this' DateRange 
        /// but not in any of the other given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public IEnumerable<DateRange> Difference(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));

            return new DifferenceOperation().Invoke(this, dateRanges);
        }

        /// <summary>
        /// Provides the difference between a DateRange value and a set of 
        /// DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'this' DateRange 
        /// but not in any of the other given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public IEnumerable<DateRange> Difference(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));

            return new DifferenceOperation().Invoke(this, dateRanges);
        }

        /// <summary>
        /// Provides the difference between two sets of DateRange values.
        /// </summary>
        /// <param name="a">A set of DateRange values.</param>
        /// <param name="b">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'a' but not in 'b'.
        /// </returns>
        public static IEnumerable<DateRange> Difference(SetOfDateRanges a, SetOfDateRanges b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a));
            if (b == null) throw new ArgumentNullException(nameof(b));

            return new DifferenceOperation().Invoke(new[] { a, b });
        }
    }
}
