using System;

namespace DateRanges
{
    /// <summary>
    /// Used to represent changes when evaluating sets of DateRanges.
    /// </summary>
    internal struct InflectionPoint
    {
        public DateTime Date { get; private set; }

        public InflectionType InflectionType { get; private set; }

        public int Key { get; private set; }

        public InflectionPoint(DateTime date, InflectionType inflectionType, int key)
        {
            Date = date;
            InflectionType = inflectionType;
            Key = key;
        }

        public override string ToString()
        {
            return $"{InflectionType} {Date.ToYMDString()} [{Key}]";
        }
    }
}