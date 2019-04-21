using System;

namespace DateRanges
{
    /// <summary>
    /// Used to represent changes when evaluating sets of DateRanges.
    /// </summary>
    internal struct InflectionPoint
    {
        public DateTime Date { get; set; }

        public InflectionType InflectionType { get; set; }

        public int SetIndex { get; set; }

        public InflectionPoint(DateTime date, InflectionType inflectionType, int setIndex)
        {
            Date = date;
            InflectionType = inflectionType;
            SetIndex = setIndex;
        }

        public override string ToString()
        {
            return $"{InflectionType} {Date.ToYMDString()} [{SetIndex}]";
        }
    }
}
