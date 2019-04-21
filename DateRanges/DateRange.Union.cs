using System.Collections.Generic;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the union of two DateRange values.
        /// </summary>
        /// <param name="value">A DateRange value.</param>
        /// <returns>A set of DateRange values representing the union of 
        /// the two DateRanges. This set could contain zero, one, or two 
        /// DateRange values.</returns>
        public IEnumerable<DateRange> Union(DateRange value)
        {
            return new UnionOperation().Invoke(this, value);
        }

        /// <summary>
        /// Calculates the union of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</param>
        /// <returns>A set of DateRange values representing the union of all 
        /// provided values.</returns>
        public static IEnumerable<DateRange> Union(params DateRange[] set)
        {
            return new UnionOperation().Invoke(set);
        }

        /// <summary>
        /// Calculates the union of all provided DateRange values.
        /// </summary>
        /// <param name="set">A set of DateRange values.</param>
        /// <returns>A set of DateRange values representing the union of all 
        /// provided values.</returns>
        public static IEnumerable<DateRange> Union(SetOfDateRanges set)
        {
            return new UnionOperation().Invoke(set);
        }

        /// <summary>
        /// Calculates the union of all DateRange values included in multiple sets.
        /// </summary>
        /// <param name="set">A collection of sets of DateRange values.</param>
        /// <returns>A set of DateRange values representing the union of all 
        /// provided values.</returns>
        public static IEnumerable<DateRange> Union(params SetOfDateRanges[] sets)
        {
            return new UnionOperation().Invoke(sets);
        }

        /// <summary>
        /// Calculates the union of all DateRange values included in multiple sets.
        /// </summary>
        /// <param name="set">A collection of sets of DateRange values.</param>
        /// <returns>A set of DateRange values representing the union of all 
        /// provided values.</returns>
        public static IEnumerable<DateRange> Union(IEnumerable<SetOfDateRanges> sets)
        {
            return new UnionOperation().Invoke(sets);
        }
    }
}
