using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
    public struct DateRange
    {
        /// <summary>
        /// The inclusive lower boundary of the date range.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// The exclusive upper boundary of the date range.
        /// </summary>
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

        /// <summary>
        /// Instantiates a new DateRange value where the start date is equal to 
        /// DateTime.MinValue.Date and the end date is equal to DateTime.MaxValue.Date.
        /// </summary>
        /// <returns>A DateRange value.</returns>
        public static DateRange Full()
        {
            return new DateRange(DateTime.MinValue, DateTime.MaxValue);
        }

        /// <summary>
        /// Indicates whether or not this DateRange value is empty, i.e. its 
        /// start date is equal to its end date giving the DateRange a length 
        /// of zero.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return StartDate == EndDate;
        }

        /// <summary>
        /// Calculates the length of the DateRange. Note that the end date of 
        /// a DateRange is exclusive.
        /// </summary>
        /// <returns>A TimeSpan value.</returns>
        public TimeSpan Length()
        {
            return EndDate - StartDate;
        }

        /// <summary>
        /// Compares this DateRange with another object for equality.
        /// </summary>
        /// <param name="obj">An object.</param>
        /// <returns>True when this and obj are equal; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is DateRange)) return false;

            return Equals((DateRange)obj);
        }

        /// <summary>
        /// Compares this DateRange with another DateRange for equality.
        /// </summary>
        /// <param name="value">A DateRange.</param>
        /// <returns>True when this equals value; otherwise, false.</returns>
        public bool Equals(DateRange value)
        {
            return StartDate == value.StartDate &&
                   EndDate == value.EndDate;
        }

        /// <summary>
        /// Calculates a hash code for this DateRange value.
        /// </summary>
        /// <returns>An integer.</returns>
        public override int GetHashCode()
        {
            var hashCode = -1134829439;
            hashCode = hashCode * -1521134295 + StartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDate.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares two DateRange values for equality.
        /// </summary>
        /// <param name="a">A DateRange value.</param>
        /// <param name="b">A DateRange value.</param>
        /// <returns>True when a and b are equal; otherwise, false.</returns>
        public static bool operator ==(DateRange a, DateRange b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two DateRange values for inequality.
        /// </summary>
        /// <param name="a">A DateRange value.</param>
        /// <param name="b">A DateRange value.</param>
        /// <returns>True when a and b are not equal; otherwise, false.</returns>
        public static bool operator !=(DateRange a, DateRange b)
        {
            return !a.Equals(b);
        }

        /// <summary>
        /// Returns a string representation of this DateRange value.
        /// </summary>
        /// <returns>A string representation of this DateRange value</returns>
        public override string ToString()
        {
            const string Format = "yyyy-MM-dd";
            return $"{{\"StartDate\":\"{StartDate.ToString(Format)}\",\"EndDate\":\"{EndDate.ToString(Format)}\"}}";
        }
    }
}
