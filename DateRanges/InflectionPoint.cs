using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
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
    }
}
