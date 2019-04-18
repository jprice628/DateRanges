using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateRanges
{
    public partial struct DateRange
    {
        /// <summary>
        /// Calculates the intersection between two DateRange values.
        /// </summary>
        /// <param name="value">A DateRange value.</param>
        /// <returns>A DateRange value representing the intersection between 
        /// the two given DateRange values. When no intersection exists, this 
        /// value will be an empty DateRange.</returns>
        public DateRange Intersect(DateRange value)
        {
            if (IsEmpty() || value.IsEmpty())
            {
                return Empty();
            }
            else if (EndDate <= value.StartDate || value.EndDate <= StartDate)
            {
                return Empty();
            }
            else
            {
                return new DateRange(
                    Date.Max(StartDate, value.StartDate),
                    Date.Min(EndDate, value.EndDate)
                    );
            }
        }
    }
}
