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
        /// <param name="value">A DateRange value.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection of the two 
        /// DateRanges. This set could contain zero or one values.
        /// </returns>
        public IEnumerable<DateRange> Intersect(DateRange value)
        {
            return new IntersectOperation().InvokeAsSeparateSets(new[] { this, value });
        }

        /// <summary>
        /// Calculates the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values representing the intersection of the 
        /// provided DateRanges. This set could contain zero or one 
        /// values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'set' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'set' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params DateRange[] set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            if (set.Length < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new IntersectOperation().InvokeAsSeparateSets(set);
        }

        /// <summary>
        /// Calculates the intersection of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</ param >
        /// <returns>
        /// A set of DateRange values representing the intersection
        /// of the provided DateRanges. This set could contain zero or one 
        /// values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'set' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when 'set' contains less than two values.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(SetOfDateRanges set)
        {
            if (set == null) throw new ArgumentNullException(nameof(set));
            if (set.Count() < 2) throw new ArgumentException("'set' must contain at least two DateRange values.");

            return new IntersectOperation().InvokeAsSeparateSets(set);
        }

        /// <summary>
        /// Calculates the intersection between multiple sets of DateRange values.
        /// </summary>
        /// <param name="sets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection between 
        /// the provided sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'sets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(params SetOfDateRanges[] sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            if (sets.Length < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new IntersectOperation().Invoke(sets);
        }

        /// <summary>
        /// Calculates the intersection between multiple sets of DateRange values.
        /// </summary>
        /// <param name="sets">Two or more sets of DateRange values.</param>
        /// <returns>
        /// A set of DateRange values representing the intersection between 
        /// the provided sets of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'sets' is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when less than two sets are provided.
        /// </exception>
        public static IEnumerable<DateRange> Intersect(IEnumerable<SetOfDateRanges> sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            if (sets.Count() < 2) throw new ArgumentException("'sets' must contain at least two sets of DateRange values.");

            return new IntersectOperation().Invoke(sets);
        }
    }
}
