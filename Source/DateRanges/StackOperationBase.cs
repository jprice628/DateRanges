using System;
using System.Collections.Generic;
using System.Linq;

namespace DateRanges
{
    /// <summary>
    /// A base class for defining stacking operations.
    /// </summary>
    /// <remarks>
    /// Occassionally, it is necessary to take items that are DateRange like 
    /// and stack them on top of one another to create one or more composites. 
    /// For example, there might be a red car that was purchased at one point 
    /// in time with GoodYear tires. Later the car was painted blue. Later 
    /// still, the tires were replaced with Firestone tires. It might be 
    /// necessary to determine the state of the car at any point in time. This 
    /// class facilitates such a determination.
    /// 
    /// Per its name, objects of this type are intended to be single operations.
    /// They are not thread safe, and should not be used-more than once.
    /// </remarks>
    /// <typeparam name="T">The type of item to be stacked.</typeparam>
    public abstract class StackOperationBase<T>
    {
        // Stores the inflection points for the current operation.
        private List<InflectionPoint<T>> inflectionPoints;

        // Stores the start date of a result DateRange during processing 
        // while its end date is still unknown.
        private DateTime startDateBuffer;

        // Stores the items that will be placed into the next stack.
        private Dictionary<int, T> itemBuffer;

        // Stores the set of DateRange values that will be returned as the 
        // final result of the operation.
        private List<Stack<T>> results;

        protected StackOperationBase()
        {
        }

        /// <summary>
        /// Invokes the operation.
        /// </summary>
        /// <param name="items">The set of items to be stacked.</param>
        /// <returns>A set of stack values.</returns>
        public IEnumerable<Stack<T>> Invoke(params T[] items)
        {
            return Invoke((IEnumerable<T>)items);
        }

        /// <summary>
        /// Invokes the operation.
        /// </summary>
        /// <param name="items">The set of items to be stacked.</param>
        /// <returns>A set of stack values.</returns>
        public IEnumerable<Stack<T>> Invoke(IEnumerable<T> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (items.Count() == 0) return Enumerable.Empty<Stack<T>>();

            Init();
            AddInflectionPoints(items);
            inflectionPoints.Sort(InflectionPointComparer);
            ProcessInflectionPoints();
            return results;
        }

        private void Init()
        {
            inflectionPoints = new List<InflectionPoint<T>>();
            results = new List<Stack<T>>();
            itemBuffer = new Dictionary<int, T>();
        }

        private void AddInflectionPoints(IEnumerable<T> items)
        {
            int key = 0;
            foreach (var item in items)
            {
                var dateRange = DateRangeForItem(item);
                if (!dateRange.IsEmpty())
                {
                    inflectionPoints.Add(new InflectionPoint<T>(dateRange.StartDate, InflectionType.DateRangeStart, key, item));
                    inflectionPoints.Add(new InflectionPoint<T>(dateRange.EndDate, InflectionType.DateRangeEnd, key, item));
                    key++;
                }
            }
        }

        /// <summary>
        /// Returns the DateRange associated with the given item.
        /// </summary>
        /// <param name="item">An item that is being stacked.</param>
        /// <returns>A DateRange value.</returns>
        protected abstract DateRange DateRangeForItem(T item);

        private static int InflectionPointComparer(InflectionPoint<T> a, InflectionPoint<T> b)
        {
            return a.Date.CompareTo(b.Date);
        }

        private void ProcessInflectionPoints()
        {
            if (inflectionPoints.Count == 0) return;

            // The trick to processing the ips is that all of the ips for a given 
            // date need to be accounted for before an outcome is determined for 
            // that date.
            startDateBuffer = inflectionPoints.First().Date;
            foreach (var ip in inflectionPoints)
            {
                if (startDateBuffer != ip.Date)
                {
                    // All of the ips for the date have been accounted for, 
                    // and the date is about to change.
                    PushBuffer(ip.Date);
                    startDateBuffer = ip.Date;
                }

                switch (ip.InflectionType)
                {
                    case InflectionType.DateRangeStart:
                        itemBuffer.Add(ip.Key, ip.State);
                        break;
                    case InflectionType.DateRangeEnd:
                        itemBuffer.Remove(ip.Key);
                        break;
                }
            }
        }

        private void PushBuffer(DateTime endDate)
        {
            if (!itemBuffer.Any()) return;

            results.Add(new Stack<T>(
                new DateRange(startDateBuffer, endDate),
                itemBuffer.Values.ToArray()
                ));
        }
    }
}
