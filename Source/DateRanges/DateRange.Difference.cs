using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the difference between two DateRange values.
        /// </summary>
        /// <param name="dateRange">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'this' DateRange but not in the 'value' DateRange.
        /// </returns>
        public IEnumerable<DateRange> Difference(DateRange dateRange)
        {
            return new DifferenceOperation().Invoke(this, dateRange);
        }

        /// <summary>
        /// Calculates the difference of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first 
        /// DateRange but not in any of the other DateRanges.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'dateRanges' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Difference(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new DifferenceOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Calculates the difference of all provided DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first 
        /// DateRange but not in any of the other DateRanges.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'dateRanges' contains 
        /// less than two values.</exception>
        public static IEnumerable<DateRange> Difference(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new DifferenceOperation().InvokeAsSeparateSets(dateRanges);
        }

        /// <summary>
        /// Calculates the difference between multiple sets of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first set of 
        /// DateRange values but not in any of the other sets.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Difference(params SetOfDateRanges[] dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Length < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new DifferenceOperation().Invoke(dateRangeSets);
        }

        /// <summary>
        /// Calculates the difference between multiple sets of DateRange values.
        /// </summary>
        /// <param name="dateRangeSets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first set of 
        /// DateRange values but not in any of the other sets.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRangeSets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Difference(IEnumerable<SetOfDateRanges> dateRangeSets)
        {
            if (dateRangeSets == null) throw new ArgumentNullException(nameof(dateRangeSets));
            if (dateRangeSets.Count() < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new DifferenceOperation().Invoke(dateRangeSets);
        }
    }
}
