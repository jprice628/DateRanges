using System;

namespace DateRanges
{
    /// <summary>
    /// Represents a range of dates or a time period, e.g. 2/14/2019 to 3/14/2019.
    /// </summary>
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
        /// Initializes a new instance of the DateRange structure.
        /// </summary>
        /// <param name="startDate">The inclusive lower boundary of the date range. Time components are ignored.</param>
        /// <param name="endDate">The exclusive upper boundary of the date range. Time components are ignored.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when startDate is greater than endDate.</exception>
        public DateRange(DateTime startDate, DateTime endDate)
        {
            var internalStartDate = Date.NewDate(startDate);
            var internalEndDate = Date.NewDate(endDate);

            if (internalStartDate > internalEndDate)
            {
                throw new ArgumentOutOfRangeException("Error while creating DateRange: start date is greater than end date.");
            }

            StartDate = internalStartDate;
            EndDate = internalEndDate;
        }

        /// <summary>
        /// Initializes a new instance of the DateRange structure with its 
        /// StartDate set to DateTime.MinValue.Date and its EndDate set to 
        /// DateTime.MaxValue.Date.
        /// </summary>
        /// <returns>A DateRange value.</returns>

        public static DateRange Full() => new DateRange(Date.MinValue, Date.MaxValue);

        /// <summary>
        /// Indicates whether or not this DateRange value is empty, i.e. its 
        /// start date is equal to its end date giving the DateRange a length 
        /// of zero.
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => Date.AreEqual(StartDate, EndDate);

        /// <summary>
        /// Calculates the length of the DateRange. Note that the end date of 
        /// a DateRange is exclusive.
        /// </summary>
        /// <returns>A TimeSpan value.</returns>
        public TimeSpan Length() => EndDate - StartDate;

        /// <summary>
        /// Determines whether or not a date is contained within this DateRange.
        /// </summary>
        /// <param name="value">A DateTime value.</param>
        /// <returns>True if the value is within this DateRange; otherwise, false.</returns>
        public bool Contains(DateTime value) => value >= StartDate && value < EndDate;

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
            return Date.AreEqual(StartDate, value.StartDate) &&
                Date.AreEqual(EndDate, value.EndDate);
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
        public static bool operator ==(DateRange a, DateRange b) => a.Equals(b);

        /// <summary>
        /// Compares two DateRange values for inequality.
        /// </summary>
        /// <param name="a">A DateRange value.</param>
        /// <param name="b">A DateRange value.</param>
        /// <returns>True when a and b are not equal; otherwise, false.</returns>
        public static bool operator !=(DateRange a, DateRange b) => !a.Equals(b);

        /// <summary>
        /// Returns a string representation of this DateRange value.
        /// </summary>
        /// <returns>A string representation of this DateRange value</returns>
        public override string ToString()
        {
            return $"{StartDate.ToYMDString()} to {EndDate.ToYMDString()}";
        }
    }
}
