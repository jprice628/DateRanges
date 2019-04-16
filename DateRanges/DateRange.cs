using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
    public struct DateRange
    {
        // The inclusive lower boundary of the date range.
        public DateTime StartDate { get; private set; }

        // The exclusive upper boundary of the date range.
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// Instantiates a new DateRange value.
        /// </summary>
        /// <param name="startDate">The inclusive lower boundary of the date range. Time components are ignored.</param>
        /// <param name="endDate">The exclusive upper boundary of the date range. Time components are ignored.</param>
        public DateRange(DateTime startDate, DateTime endDate)
        {
            var internalStartDate = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Unspecified);
            var internalEndDate = DateTime.SpecifyKind(endDate.Date, DateTimeKind.Unspecified);

            if (internalStartDate > internalEndDate)
            {
                throw new ArgumentOutOfRangeException("Error while creating DateRange: start date is greater than end date.");
            }

            StartDate = internalStartDate;
            EndDate = internalEndDate;
        }

        public static bool operator ==(DateRange a, DateRange b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(DateRange a, DateRange b)
        {
            return !a.Equals(b);
        }

        public override string ToString()
        {
            const string Format = "yyyy-MM-dd";
            return $"{{\"StartDate\":\"{StartDate.ToString(Format)}\",\"EndDate\":\"{EndDate.ToString(Format)}\"}}";
        }
    }
}
