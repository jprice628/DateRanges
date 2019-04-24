using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Provides the absolute compliment for a DateRange value.
        /// </summary>
        /// <returns>
        /// A set of DateRange values containing all dates that are not in 
        /// 'this' DateRange value.
        /// </returns>
        public SetOfDateRanges Compliment()
        {
            return Full().Difference(this);
        }

        /// <summary>
        /// Provides the absolute compliment for a set of DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRangeValues.</param>
        /// <returns>
        /// A set of DateRange values containing all dates that are not in 
        /// the given set of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static SetOfDateRanges Compliment(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));

            return Full().Difference(dateRanges);
        }

        /// <summary>
        /// Provides the absolute compliment for a set of DateRange values.
        /// </summary>
        /// <param name="dateRanges">A set of DateRangeValues.</param>
        /// <returns>
        /// A set of DateRange values containing all dates that are not in 
        /// the given set of DateRange values.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when 'dateRanges' is null.
        /// </exception>
        public static SetOfDateRanges Compliment(params DateRange[] dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));

            return Full().Difference(dateRanges);
        }
    }
}
