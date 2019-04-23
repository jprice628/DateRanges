using System;
using System.Collections.Generic;
using DateRanges;
using System.Linq;

namespace Sample1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Jeff, John, Sally, and Jane travel frequently to Atlanta for 
            // business, so they decide to lease and share an apartment. The 
            // following arrays of DateRange values represent the time that each 
            // has spent at the apartment.
            var jeff = new[]
            {
                new DateRange(Date.NewDate(2019, 1,  7), Date.NewDate(2019, 1, 12)),
                new DateRange(Date.NewDate(2019, 1, 22), Date.NewDate(2019, 1, 24)),
                new DateRange(Date.NewDate(2019, 2, 13), Date.NewDate(2019, 2, 17))
            };
            var john = new[]
            {
                new DateRange(Date.NewDate(2019, 1, 14), Date.NewDate(2019, 1, 19)),
                new DateRange(Date.NewDate(2019, 1, 29), Date.NewDate(2019, 1, 31)),
                new DateRange(Date.NewDate(2019, 2, 11), Date.NewDate(2019, 2, 15))
            };
            var sally = new[]
            {
                new DateRange(Date.NewDate(2019, 1, 28), Date.NewDate(2019, 2,  2)),
                new DateRange(Date.NewDate(2019, 1, 14), Date.NewDate(2019, 1, 17)),
            };
            var jane = new[]
            {
                new DateRange(Date.NewDate(2019, 2,  4), Date.NewDate(2019, 2,  9))
            };

            // Problem 1: When was the apartment in-use? This can be done by 
            // performing a union on the arrays above.
            var inUse = DateRange.Union(jeff, john, sally, jane);
            var totalDaysInUse = inUse.Sum(x => x.Length().TotalDays);

            Console.WriteLine("Problem 1: In-Use.");
            PrintDateRanges(inUse);
            Console.WriteLine("Total Days: " + totalDaysInUse);
            Console.WriteLine();

            // Problem 2: For what periods of time did John and Sally share 
            // the apartment? This can be determined using the Intersect operation.
            var johnAndSally = DateRange.Intersect(john, sally);

            Console.WriteLine("Problem 2: When did John and Sally share the apartment?");
            PrintDateRanges(johnAndSally);
            Console.WriteLine();

            // Problem 3: When was the apartment not in use between 1/1 and 2/28?
            // This can be determined using the Difference operation.
            var notInUse = DateRange.Difference(
                new[] { new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 2, 28)) },
                inUse
                );
            var totalDaysNotInUse = notInUse.Sum(x => x.Length().TotalDays);

            Console.WriteLine("Problem 3: Not In-Use.");
            PrintDateRanges(notInUse);
            Console.WriteLine("Total Days: " + totalDaysNotInUse);
            Console.WriteLine();

            // Problem 4: Who occupied the apartment for each period of time? 
            // This can be done using the Stack operation.
            var individualStays = jeff.Select(x => new Tuple<string, DateRange>("Jeff", x))
                .Union(john.Select(x => new Tuple<string, DateRange>("John", x)))
                .Union(sally.Select(x => new Tuple<string, DateRange>("Sally", x)))
                .Union(jane.Select(x => new Tuple<string, DateRange>("Jane", x)));
            var stackedStays = new StackStaysOperation().Invoke(individualStays);

            Console.WriteLine("Problem 4: Who occupied the apartment for each period of time?");
            PrintStackedStays(stackedStays);
            Console.WriteLine();
        }

        static void PrintDateRanges(IEnumerable<DateRange> dateRanges)
        {
            foreach (var dateRange in dateRanges)
            {
                Console.WriteLine(ToString(dateRange));
            }
        }

        static void PrintStackedStays(IEnumerable<StackedItems<Tuple<string, DateRange>>> stackedStays)
        {
            foreach (var stack in stackedStays)
            {
                Console.Write(ToString(stack.DateRange) + ": ");
                foreach (var person in stack)
                {
                    Console.Write(person.Item1 + " ");
                }
                Console.WriteLine();
            }
        }

        static string ToString(DateRange dateRange)
        {
            return $"{dateRange.StartDate.ToString("M/d")} to {dateRange.EndDate.ToString("M/d")}";
        }

        class StackStaysOperation : StackOperationBase<Tuple<string, DateRange>>
        {
            protected override DateRange DateRangeForItem(Tuple<string, DateRange> individualStay)
            {
                return individualStay.Item2;
            }
        }
    }
}
