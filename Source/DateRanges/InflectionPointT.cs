using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
    /// <summary>
    /// Used to represent changes when evaluating sets of DateRanges.
    /// </summary>
    internal struct InflectionPoint<T>
    {
        public DateTime Date { get; private set; }

        public InflectionType InflectionType { get; private set; }

        public int Key { get; private set; }

        public T State { get; private set; }

        public InflectionPoint(DateTime date, InflectionType inflectionType, int key, T state)
        {
            Date = date;
            InflectionType = inflectionType;
            Key = key;
            State = state;
        }

        public override string ToString()
        {
            return $"{InflectionType} {Date.ToYMDString()} [{Key}] {State}";
        }
    }
}
