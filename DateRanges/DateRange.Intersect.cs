using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateRanges
{
    public partial struct DateRange
    {
        public IEnumerable<DateRange> Intersect(DateRange value)
        {
            if (IsEmpty() || value.IsEmpty())
            {
                return Enumerable.Empty<DateRange>();
            }
            else if (EndDate <= value.StartDate || value.EndDate <= StartDate)
            {
                return Enumerable.Empty<DateRange>();
            }
            else
            {
                return new[]
                {
                    new DateRange(
                        Date.Max(StartDate, value.StartDate),
                        Date.Min(EndDate, value.EndDate)
                        )
                };
            }
        }
    }
}
