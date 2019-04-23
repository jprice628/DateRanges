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
        /// <param name="value">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values containing each day in 'this' DateRange but not in the 'value' DateRange.
        /// </returns>
        public IEnumerable<DateRange> Difference(DateRange value)
        {
            return new DifferenceOperation().InvokeAsSeparateSets(new[] { this, value });
        }

        /// <summary>
        /// Calculates the difference of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first 
        /// DateRange but not in any of the other DateRanges.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'set' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'set' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Difference(params DateRange[] set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            if (set.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new DifferenceOperation().InvokeAsSeparateSets(set);
        }

        /// <summary>
        /// Calculates the difference of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first 
        /// DateRange but not in any of the other DateRanges.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'set' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'set' contains 
        /// less than two values.</exception>
        public static IEnumerable<DateRange> Difference(SetOfDateRanges set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            if (set.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new DifferenceOperation().InvokeAsSeparateSets(set);
        }

        /// <summary>
        /// Calculates the difference between multiple sets of DateRange values.
        /// </summary>
        /// <param name="sets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first set of 
        /// DateRange values but not in any of the other sets.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'sets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Difference(params SetOfDateRanges[] sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            if (sets.Length < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new DifferenceOperation().Invoke(sets);
        }

        /// <summary>
        /// Calculates the difference between multiple sets of DateRange values.
        /// </summary>
        /// <param name="sets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values containing each day in the first set of 
        /// DateRange values but not in any of the other sets.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'sets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Difference(IEnumerable<SetOfDateRanges> sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            if (sets.Count() < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new DifferenceOperation().Invoke(sets);
        }
    }
}
