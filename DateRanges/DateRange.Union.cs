using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the union between two DateRange values.
        /// </summary>
        /// <param name="value">A DateRange value.</param>
        /// <returns>A set of DateRange values representing the union between 
        /// the two DateRanges. This set could contain zero, one, or two 
        /// DateRange values.</returns>
        public IEnumerable<DateRange> Union(DateRange value)
        {
            if (IsEmpty())
            {
                return value.IsEmpty() ? Enumerable.Empty<DateRange>() : new[] { value };
            }
            else if (value.IsEmpty())
            {
                return new[] { this };
            }
            else if (EndDate < value.StartDate || value.EndDate < StartDate)
            {
                return new[] { this, value };
            }
            else
            {
                var dr = new DateRange(
                    Date.Min(StartDate, value.StartDate),
                    Date.Max(EndDate, value.EndDate)
                    );
                return new[] { dr };                
            }
        }
    }
}
