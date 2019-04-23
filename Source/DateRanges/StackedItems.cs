using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DateRanges
{
    /// <summary>
    /// Represents a collection of items that overlap each other with regards to time.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct StackedItems<T> : IEnumerable<T>
    {
        private IEnumerable<T> items;

        public DateRange DateRange { get; private set; }

        public StackedItems(DateRange dateRange, IEnumerable<T> items)
        {
            if (dateRange.IsEmpty()) throw new ArgumentException("'dateRange' cannot be empty.");
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (items.Count() == 0) throw new ArgumentException("'items' cannot be empty.");

            DateRange = dateRange;
            this.items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}
