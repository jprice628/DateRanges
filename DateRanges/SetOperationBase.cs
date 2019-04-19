using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DateRanges
{
    /// <summary>
    /// Provides base functionality for set operations, i.e. Union, 
    /// Intersect, and Difference.
    /// </summary>
    /// <remarks>
    /// The class supplies a process by which one or more sets of DateRange 
    /// values are aggregated into a single set of DateRange values according 
    /// to logic defined within derived classes. 
    /// 
    /// This process begins by converting each DateRange value into two 
    /// InflectionPoint values, one representing the start of the DateRange and
    /// the other representing its end. The inflection points are then sorted
    /// in ascending order and processed in groups by date. As each date is 
    /// completed, the outcome for that date is requested from the derived class
    /// and used to determine when to start and end the DateRange values that 
    /// make up the final result of the operation.
    /// 
    /// Per its name, objects of this type are intended to be single operations.
    /// They are not thread safe, and should not be used-more than once.
    /// </remarks>
    internal abstract class SetOperationBase
    {
        // Stores the date that is currently being processed as the operation 
        // iterates through a series of inflection points generated from one 
        // or more sets of date ranges.
        private DateTime processDate;

        // Maintains the current number of DateRange values for each set that 
        // contain the date being processed. Each time a start date is encounted, 
        // the value is incrememted. Each time an end date is encountered, the 
        // value is decremented.
        private int[] numDateRangesPerSet;

        // The outcome of the last date that was processed.
        private bool latestOutcome;

        // Stores the start date of a result DateRange during processing 
        // while its end date is still unknown.
        private DateTime startDate;

        // Stores the set of DateRange values that will be returned as the 
        // final result of the operation.
        private List<DateRange> results;

        protected SetOperationBase()
        {
        }

        /// <summary>
        /// Invokes the operation for a single set of DateRange values.
        /// </summary>
        /// <param name="values">A set of DateRange values.</param>
        /// <returns>A set of DateRange values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when values is null.</exception>
        public IEnumerable<DateRange> Invoke(IEnumerable<DateRange> values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Count() == 0) return Enumerable.Empty<DateRange>();

            Init(1);
            var ips = ToInflectionPoints(values)
                .OrderBy(x => x.Date);
            ProcessInflectionPoints(ips);
            return results;
        }

        /// <summary>
        /// Invokes the operation for multiple sets of DateRange values.
        /// </summary>
        /// <param name="values">An array containing multiple sets of DateRange values.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown when values is null.</exception>
        /// <exception cref="ArgumentException">Thrown when less than two sets of DateRange values are provided.</exception>
        public IEnumerable<DateRange> Invoke(params IEnumerable<DateRange>[] values)
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Count() < 2) throw new ArgumentException("Error performing set operation on multiple sets: At least two sets must be provided.");

            Init(values.Length);
            var ips = ToInflectionPoints(values)
                .OrderBy(x => x.Date);
            ProcessInflectionPoints(ips);
            return results;
        }

        private void Init(int numStates)
        {
            numDateRangesPerSet = InitSetStates(numStates);
            latestOutcome = false;
            results = new List<DateRange>();
        }

        private int[] InitSetStates(int numStates)
        {
            var arr = new int[numStates];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
            return arr;
        }

        private static IEnumerable<InflectionPoint> ToInflectionPoints(params IEnumerable<DateRange>[] sets)
        {
            var result = new List<InflectionPoint>();
            for (var i = 0; i < sets.Length; i++)
            {
                foreach (var dateRange in sets[i])
                {
                    result.AddRange(ToInflectionPoints(dateRange, i));
                }
            }
            return result;
        }

        private static IEnumerable<InflectionPoint> ToInflectionPoints(DateRange value, int setIndex)
        {
            if (value.IsEmpty()) return Enumerable.Empty<InflectionPoint>();

            return new[]
            {
                new InflectionPoint(value.StartDate, InflectionType.DateRangeStart, setIndex),
                new InflectionPoint(value.EndDate, InflectionType.DateRangeEnd, setIndex)
            };
        }

        private void ProcessInflectionPoints(IEnumerable<InflectionPoint> ips)
        {
            if (ips.Count() == 0) return;

            // The trick to processing the ips is that all of the ips for a given 
            // date need to be accounted for before an outcome is determined for 
            // that date.

            processDate = ips.First().Date;
            foreach (var ip in ips)
            {
                if (processDate != ip.Date)
                {
                    // All of the ips for the date have been accounted for, 
                    // and the date is about to change.
                    HandleNewOutcome(DetermineOutcome(numDateRangesPerSet));
                    processDate = ip.Date;
                }

                switch (ip.InflectionType)
                {
                    case InflectionType.DateRangeStart:
                        numDateRangesPerSet[ip.SetIndex]++;
                        break;
                    case InflectionType.DateRangeEnd:
                        numDateRangesPerSet[ip.SetIndex]--;
                        break;
                }
            }

            // There's always one final outcome that isn't caught by the block 
            // above and needs to be determined.
            HandleNewOutcome(DetermineOutcome(numDateRangesPerSet));
        }

        private bool DetermineOutcome(int[] setStates)
        {
            return setStates.Length == 1 ? DetermineSingleSetOutcome(setStates[0]) : DetermineMultiSetOutcome(setStates);
        }

        /// <summary>
        /// Overridden in derived classes to determine the outcome for a single 
        /// set of DateRange values based on the number of DateRange values 
        /// containing a particular date.
        /// </summary>
        /// <param name="numDateRanges">The number of DateRange values that 
        /// contain the date in question.</param>
        /// <returns></returns>
        protected abstract bool DetermineSingleSetOutcome(int numDateRanges);

        /// <summary>
        /// Overridden in derived classes to determine the outcome for multiple 
        /// sets of DateRange values based on the number of DateRange values in 
        /// each set that contain a particular date.
        /// </summary>
        /// <param name="numActiveDateRanges">
        /// The number of DateRange values 
        /// whose start date less than or equal to the date in question and 
        /// whose end date is greater than the date in question.
        /// </param>
        /// <returns></returns>
        protected abstract bool DetermineMultiSetOutcome(int[] numDateRangePerSet);

        private void HandleNewOutcome(bool newOutcome)
        {
            if (newOutcome == latestOutcome) return;

            if (newOutcome)
            {
                startDate = processDate;
            }
            else
            {
                results.Add(new DateRange(startDate, processDate));
            }
            latestOutcome = newOutcome;
        }
    }
}
