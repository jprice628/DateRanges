using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Provides the intersection between two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in both 
        /// given DateRange values.
        /// </returns>
        public IEnumerable<DateRange> Intersect(DateRange dateRange)
        {
            return new IntersectOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Provides the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values containing each date that exists in all 
        /// of the given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Length == 0) return Enumerable.Empty<DateRange>();

            return new IntersectOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Provides the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values containing each date that exists in all 
        /// of the given DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() == 0) return Enumerable.Empty<DateRange>();

            return new IntersectOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Provides the intersection between each set of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in all 
        /// of the given sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params SetOfDateRanges[] dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Length == 0) return Enumerable.Empty<DateRange>();

            return new IntersectOperation().Invoke(dateRangeSets);
        }

        /// <summary>
        /// Provides the intersection between each set of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each date that exists in all 
        /// of the given sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(IEnumerable<SetOfDateRanges> dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Count() == 0) return Enumerable.Empty<DateRange>();

            return new IntersectOperation().Invoke(dateRangeSets);
        }
    }
}
