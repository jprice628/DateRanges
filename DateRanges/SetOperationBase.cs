using System;
using System.Collections.Generic;
using System.Linq;

using SetOfDateRanges = System.Collections.Generic.IEnumerable<DateRanges.DateRange>;

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
        // Stores the inflection points for the current operation.
        private List<InflectionPoint> inflectionPoints;

        // Stores the date that is currently being processed as the operation 
        // iterates through a series of inflection points generated from one 
        // or more sets of date ranges.
        private DateTime processDate;

        // Maintains the current number of DateRange values for each set that 
        // contain the date being processed. Each time a start date is encounted, 
        // the value is incrememted. Each time an end date is encountered, the 
        // value is decremented.
        private int[] setStates;

        // The outcome of the last date that was processed.
        private bool latestOutcome;

        // Stores the start date of a result DateRange during processing 
        // while its end date is still unknown.
        private DateTime startDateBuffer;

        // Stores the set of DateRange values that will be returned as the 
        // final result of the operation.
        private List<DateRange> results;

        protected SetOperationBase()
        {
        }

        /// <summary>
        /// Invokes the operation.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>A set of DateRange values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dateRanges is null.</exception>
        internal SetOfDateRanges Invoke(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() == 0) return Enumerable.Empty<DateRange>();

            Init(1);
            AddInflectionPoints(dateRanges, 0);
            inflectionPoints.Sort(InflectionPointComparer);
            ProcessInflectionPoints();
            return results;
        }

        /// <summary>
        /// Invokes the operation, treating each DateRange value as if it were part of a different set.
        /// </summary>
        /// <param name="dateRanges">A set of DateRange values.</param>
        /// <returns>A set of DateRange values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when dateRanges is null.</exception>
        internal SetOfDateRanges InvokeAsSeparateSets(SetOfDateRanges dateRanges)
        {
            if (dateRanges == null) throw new ArgumentNullException(nameof(dateRanges));
            if (dateRanges.Count() == 0) return Enumerable.Empty<DateRange>();

            Init(dateRanges.Count());
            AddInflectionPointsAsSeparateSets(dateRanges);
            inflectionPoints.Sort(InflectionPointComparer);
            ProcessInflectionPoints();
            return results;
        }

        /// <summary>
        /// Invokes the operation.
        /// </summary>
        /// <param name="sets">A collections of DateRange value sets.</param>
        /// <returns>A set of DateRange values.</returns>
        /// <exception cref="ArgumentNullException">Thrown when values is null.</exception>
        internal SetOfDateRanges Invoke(IEnumerable<SetOfDateRanges> sets)
        {
            if (sets == null) throw new ArgumentNullException(nameof(sets));
            if (sets.Count() == 0) return Enumerable.Empty<DateRange>();

            Init(sets.Count());
            AddInflectionPoints(sets);
            inflectionPoints.Sort(InflectionPointComparer);
            ProcessInflectionPoints();
            return results;
        }

        private void Init(int numSets)
        {
            inflectionPoints = new List<InflectionPoint>();
            setStates = InitIntArray(numSets);
            latestOutcome = false;
            results = new List<DateRange>();
        }

        private static int[] InitIntArray(int size)
        {
            var arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
            return arr;
        }

        private void AddInflectionPoints(IEnumerable<SetOfDateRanges> sets)
        {
            int setIndex = 0;
            foreach(var set in sets)
            {
                AddInflectionPoints(set, setIndex);
                setIndex++;
            }
        }

        private void AddInflectionPoints(SetOfDateRanges set, int setIndex)
        {
            foreach (var dateRange in set)
            {
                AddInflectionPoints(dateRange, setIndex);
            }
        }

        private void AddInflectionPoints(DateRange value, int setIndex)
        {
            if (value.IsEmpty()) return;

            inflectionPoints.Add(new InflectionPoint(value.StartDate, InflectionType.DateRangeStart, setIndex));
            inflectionPoints.Add(new InflectionPoint(value.EndDate, InflectionType.DateRangeEnd, setIndex));
        }

        private void AddInflectionPointsAsSeparateSets(SetOfDateRanges dateRanges)
        {
            int setIndex = 0;
            foreach (var dateRange in dateRanges)
            {
                AddInflectionPoints(dateRange, setIndex);
                setIndex++;
            }
        }

        private static int InflectionPointComparer(InflectionPoint a, InflectionPoint b)
        {
            return a.Date.CompareTo(b.Date);
        }

        private void ProcessInflectionPoints()
        {
            if (inflectionPoints.Count == 0) return;

            // The trick to processing the ips is that all of the ips for a given 
            // date need to be accounted for before an outcome is determined for 
            // that date.

            processDate = inflectionPoints.First().Date;
            foreach (var ip in inflectionPoints)
            {
                if (processDate != ip.Date)
                {
                    // All of the ips for the date have been accounted for, 
                    // and the date is about to change.
                    HandleNewOutcome(DetermineOutcome(setStates));
                    processDate = ip.Date;
                }

                switch (ip.InflectionType)
                {
                    case InflectionType.DateRangeStart:
                        setStates[ip.SetIndex]++;
                        break;
                    case InflectionType.DateRangeEnd:
                        setStates[ip.SetIndex]--;
                        break;
                }
            }

            // There's always one final outcome that isn't caught by the block 
            // above and needs to be determined.
            HandleNewOutcome(DetermineOutcome(setStates));
        }

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
        protected abstract bool DetermineOutcome(int[] setStates);

        private void HandleNewOutcome(bool newOutcome)
        {
            if (newOutcome == latestOutcome) return;

            if (newOutcome)
            {
                startDateBuffer = processDate;
            }
            else
            {
                results.Add(new DateRange(startDateBuffer, processDate));
            }

            latestOutcome = newOutcome;
        }
    }
}
