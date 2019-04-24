using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Provides the union of two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in 
        /// either of the given DateRange values.
        /// </returns>
        public IEnumerable<DateRange> Union(DateRange dateRange)
        {
            return new UnionOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Provides the union of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in 
        /// any of the given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static IEnumerable<DateRange> Union(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Length == 0) return Enumerable.Empty<DateRange>();

            return new UnionOperation().Invoke(dateRanges);
        }

        /// <summary>
        /// Provides the union of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in 
        /// any of the given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static IEnumerable<DateRange> Union(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() == 0) return Enumerable.Empty<DateRange>();

            return new UnionOperation().Invoke(dateRanges);
        }
    }
}
