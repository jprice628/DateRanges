using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the intersection between two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection of the two 
        /// DateRanges. This set could contain zero or one values.
        /// </returns>
        public IEnumerable<DateRange> Intersect(DateRange dateRange)
        {
            return new IntersectOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Calculates the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values representing the intersection of the 
        /// provided DateRanges. This set could contain zero or one 
        /// values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'dateRanges' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Length < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new IntersectOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Calculates the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values representing the intersection
        /// of the provided DateRanges. This set could contain zero or one 
        /// values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'dateRanges' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new IntersectOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Calculates the intersection between multiple sets of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection between 
        /// the provided sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params SetOfDateRanges[] dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Length < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new IntersectOperation().Invoke(dateRangeSets);
        }

        /// <summary>
        /// Calculates the intersection between multiple sets of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection between 
        /// the provided sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(IEnumerable<SetOfDateRanges> dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Count() < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new IntersectOperation().Invoke(dateRangeSets);
        }
    }
}
