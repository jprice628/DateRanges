using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the union of two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values representing the union of the two DateRanges. 
        /// This set could contain zero, one, or two DateRange values.
        /// </returns>
        public IEnumerable<DateRange> Union(DateRange dateRange)
        {
            return new UnionOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Calculates the union of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the union of all provided values.
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
        /// Calculates the union of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the union of all provided values.
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

        /// <summary>
        /// Calculates the union of all DateRange values included in multiple sets.
        /// </summary>
        /// <param name="dateRangeSets">A collection of sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the union of all provided values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        public static IEnumerable<DateRange> Union(params SetOfDateRanges[] dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Length == 0) return Enumerable.Empty<DateRange>();

            return new UnionOperation().Invoke(dateRangeSets);
        }

        /// <summary>
        /// Calculates the union of all DateRange values included in multiple sets.
        /// </summary>
        /// <param name="dateRangeSets">A collection of sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the union of all provided values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'sets' is null.
        /// </exception>
        public static IEnumerable<DateRange> Union(IEnumerable<SetOfDateRanges> dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Count() == 0) return Enumerable.Empty<DateRange>();

            return new UnionOperation().Invoke(dateRangeSets);
        }
    }
}
