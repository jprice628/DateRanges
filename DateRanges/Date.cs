using System;
using System.Collections.Generic;
using System.Text;

namespace DateRanges
{
    public static class Date
    {
        public static DateTime NewDate(int year, int month, int day)
        {
            return new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        public static DateTime NewDate(DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, DateTimeKind.Unspecified);
        }

        //public static DateTime Today();
        //public static DateTime MaxValue();
        //public static DateTime MinValue();
        //public static bool IsDate(DateTime value);
        //public static bool IsMaxDate(DateTime value);
        //public static bool IsMinDate(DateTime value);
        //public static bool IsToday(DateTime value);
        //public static int Compare(DateTime a, DateTime b);
        //public static DateTime Clamp(DateTime value, DateTime min, DateTime max);
        //public static DateTime Min(DateTime a, DateTime b);
        //public static DateTime Max(DateTime a, DateTime b);
        //public static DateTime MaxCoalesce(DateTime? value);
        //public static DateTime MinCoalesce(DateTime? value);
    }
}
