using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DateRangeIntersectTests
    {
        [TestMethod]
        public void DateRange_IntersectBasic_EmptyEmpty()
        {
            // Arrange
            var empty1 = DateRange.Empty();
            var empty2 = DateRange.Empty();

            // Act
            var result = empty1.Intersect(empty2);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_EmptyFirst()
        {
            // Arrange
            var empty = DateRange.Empty();
            var dr0101_1231 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));

            // Act
            var result = empty.Intersect(dr0101_1231);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_EmptySecond()
        {
            // Arrange
            var dr0101_1231 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));
            var empty = DateRange.Empty();

            // Act
            var result = dr0101_1231.Intersect(empty);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_FirstLessThanSecond()
        {
            // Scenario:
            // a: |-------|
            // b:           |------------|
            // r: 
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr0601_0901 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));

            // Act
            var result = dr0101_0301.Intersect(dr0601_0901);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_SecondLessThanFirst()
        {
            // Scenario:
            // a:             |-------|
            // b: |---------|
            // r: 
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr0601_0901 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));

            // Act
            var result = dr0601_0901.Intersect(dr0101_0301);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_FirstLessThanSecondContiguous()
        {
            // Scenario:
            // a: |-------|
            // b:         |------------|
            // r: 
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));

            // Act
            var result = dr0101_0301.Intersect(dr0301_0601);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_SecondLessThanFirstContiguous()
        {
            // Scenario:
            // a:           |-------|
            // b: |---------|
            // r: 

            // Arrange
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));

            // Act
            var result = dr0301_0601.Intersect(dr0101_0301);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void DateRange_IntersectBasic_FirstContainsSecond()
        {
            // Scenario:
            // a: |---------------|
            // b:    |---------|
            // r:    |---------|

            // Arrange
            var dr0101_1201 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));
            var dr0301_0901 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));

            // Act
            var result = dr0101_1201.Intersect(dr0301_0901);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0301_0901));
        }

        [TestMethod]
        public void DateRange_IntersectBasic_SecondContainsFirst()
        {
            // Scenario:
            // a:    |--------|
            // b: |--------------|
            // r: |--------------|

            // Arrange
            var dr0301_0901 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var dr0101_1201 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));

            // Act
            var result = dr0301_0901.Intersect(dr0101_1201);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0301_0901));
        }

        [TestMethod]
        public void DateRange_IntersectBasic_RightIntersect()
        {
            // Scenario:
            // a: |----------|
            // b:      |---------|
            // r:      |-----|

            // Arrange
            var dr0101_0601 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));
            var dr0301_0901 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));

            // Act
            var result = dr0101_0601.Intersect(dr0301_0901);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0301_0601));
        }

        [TestMethod]
        public void DateRange_IntersectBasic_LeftIntersect()
        {
            // Scenario:
            // a:     |----------|
            // b: |---------|
            // r:     |-----|

            // Arrange
            var dr0601_1201 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var dr0301_0901 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var dr0601_0901 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));

            // Act
            var result = dr0601_1201.Intersect(dr0301_0901);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0601_0901));
        }

        [TestMethod]
        public void DateRange_IntersectSingleSet()
        {
            // 1/1     2/1     3/1     4/1     5/1     6/1
            //  |-------------------------------|
            //          .       |-----------------------|
            //          |---------------|   
            // Result:          .       .
            //                  |-------|

            // Arrange
            var dr0101_0501 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 5, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var dr0201_0401 = new DateRange(Date.NewDate(2019, 2, 1), Date.NewDate(2019, 4, 1));
            // The following are only part of the result set.
            var dr0301_0401 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 4, 1));

            var set = new List<DateRange>
            {
                dr0101_0501,
                dr0301_0601,
                dr0201_0401
            };

            // Act - IEnumerable<DateRange> DateRange.Union(SetOfDateRanges set)
            var result = DateRange.Intersect(set);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0301_0401));
        }

        [TestMethod]
        public void DateRange_UnionSingleSetParams()
        {
            // 1/1     2/1     3/1     4/1     5/1     6/1
            //  |-------------------------------|
            //          .       |-----------------------|
            //          |---------------|   
            // Result:          .       .
            //                  |-------|

            // Arrange
            var dr0101_0501 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 5, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var dr0201_0401 = new DateRange(Date.NewDate(2019, 2, 1), Date.NewDate(2019, 4, 1));
            // The following are only part of the result set.
            var dr0301_0401 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 4, 1));

            // Act - IEnumerable<DateRange> DateRange.Union(SetOfDateRanges set)
            var result = DateRange.Intersect(
                dr0101_0501,
                dr0301_0601,
                dr0201_0401
                );

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.Contains(dr0301_0401));
        }

        [TestMethod]
        public void DateRange_IntersectMultiSet()
        {
            // 1/1 1/10  1/20 2/1         3/1  4/1    4/15        6/1     8/1
            //  |   |-----|    |-----------|    |------|           |-------|
            //      .     .    . 2/10   2/20    .     4/15   5/1   .  7/1  .    9/1
            //      .     .    .  |------| .    .      |------|    .   |---------|
            // Result:    .    .  .      . .    .      .      .    .   .   .     .
            //                    |------|                             |---|      

            // Arrange
            var empty0101 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 1, 1));
            var dr0110_0120 = new DateRange(Date.NewDate(2019, 1, 10), Date.NewDate(2019, 1, 20));
            var dr0201_0301 = new DateRange(Date.NewDate(2019, 2, 1), Date.NewDate(2019, 3, 1));
            var dr0210_0220 = new DateRange(Date.NewDate(2019, 2, 10), Date.NewDate(2019, 2, 20));
            var dr0401_0415 = new DateRange(Date.NewDate(2019, 4, 1), Date.NewDate(2019, 4, 15));
            var dr0415_0501 = new DateRange(Date.NewDate(2019, 4, 15), Date.NewDate(2019, 5, 1));
            var dr0601_0801 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 8, 1));
            var dr0701_0901 = new DateRange(Date.NewDate(2019, 7, 1), Date.NewDate(2019, 9, 1));
            // The following are only part of the result set.
            var dr0701_0801 = new DateRange(Date.NewDate(2019, 7, 1), Date.NewDate(2019, 8, 1));

            var sets = new List<IEnumerable<DateRange>>
            {
                new[] { empty0101, dr0110_0120, dr0201_0301, dr0401_0415, dr0601_0801 },
                new[] { dr0210_0220, dr0415_0501, dr0701_0901 }
            };

            // Act - IEnumerable<DateRange> DateRange.Union(IEnumerable<SetOfDateRanges> sets)
            var result = DateRange.Intersect(sets);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(dr0210_0220));
            Assert.IsTrue(result.Contains(dr0701_0801));
        }

        [TestMethod]
        public void DateRange_IntersectMultiSetParams()
        {
            // 1/1 1/10  1/20 2/1         3/1  4/1    4/15        6/1     8/1
            //  |   |-----|    |-----------|    |------|           |-------|
            //      .     .    . 2/10   2/20    .     4/15   5/1   .  7/1  .    9/1
            //      .     .    .  |------| .    .      |------|    .   |---------|
            // Result:    .    .  .      . .    .      .      .    .   .   .     .
            //                    |------|                             |---|      

            // Arrange
            var empty0101 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 1, 1));
            var dr0110_0120 = new DateRange(Date.NewDate(2019, 1, 10), Date.NewDate(2019, 1, 20));
            var dr0201_0301 = new DateRange(Date.NewDate(2019, 2, 1), Date.NewDate(2019, 3, 1));
            var dr0210_0220 = new DateRange(Date.NewDate(2019, 2, 10), Date.NewDate(2019, 2, 20));
            var dr0401_0415 = new DateRange(Date.NewDate(2019, 4, 1), Date.NewDate(2019, 4, 15));
            var dr0415_0501 = new DateRange(Date.NewDate(2019, 4, 15), Date.NewDate(2019, 5, 1));
            var dr0601_0801 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 8, 1));
            var dr0701_0901 = new DateRange(Date.NewDate(2019, 7, 1), Date.NewDate(2019, 9, 1));
            // The following are only part of the result set.
            var dr0701_0801 = new DateRange(Date.NewDate(2019, 7, 1), Date.NewDate(2019, 8, 1));

            var set1 = new[] { empty0101, dr0110_0120, dr0201_0301, dr0401_0415, dr0601_0801 };
            var set2 = new[] { dr0210_0220, dr0415_0501, dr0701_0901 };

            // Act - IEnumerable<DateRange> DateRange.Union(IEnumerable<SetOfDateRanges> sets)
            var result = DateRange.Intersect(set1, set2);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(dr0210_0220));
            Assert.IsTrue(result.Contains(dr0701_0801));
        }
    }
}
