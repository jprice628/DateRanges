using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateRanges;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DateRangeIntersectTests
    {
        [TestMethod]
        public void DateRange_Intersect_ReturnsEmptyWhenDr1IsEmpty()
        {
            // Arrange
            var dr1 = DateRange.Empty();
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.IsTrue(intersection.IsEmpty());
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsEmptyWhenDr2IsEmpty()
        {
            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31)); 
            var dr2 = DateRange.Empty();

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.IsTrue(intersection.IsEmpty());
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsEmptyWhenDr1LessThanDr2()
        {
            // Scenario:
            // dr1    |-------|
            // dr2            |------------|
            // Result [Empty]
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 31));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.IsTrue(intersection.IsEmpty());
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsEmptyWhenDr2LessThanDr1()
        {
            // Scenario:
            // dr1              |-------|
            // dr2    |---------|
            // Result [Empty]
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 31));
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.IsTrue(intersection.IsEmpty());
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsWhenDr1ContainsDr2()
        {
            // Scenario:
            // dr1  |----------------|
            // dr2      |---------|
            // Result   |---------|

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.AreEqual(dr2, intersection);
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsWhenDr2ContainsDr1()
        {
            // Scenario:
            // dr1      |--------|
            // dr2    |-------------|
            // Result   |--------|

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.AreEqual(dr1, intersection);
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsWhenDr1LeftIntersectsDr2()
        {
            // Scenario:
            // dr1      |--------|
            // dr2         |---------|
            // Result      |-----|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var expected = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.AreEqual(expected, intersection);
        }

        [TestMethod]
        public void DateRange_Intersect_ReturnsWhenDr1RightIntersectsDr2()
        {
            // Scenario:
            // dr1          |--------|
            // dr2      |---------|
            // Result       |-----|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var expected = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));

            // Act
            var intersection = dr1.Intersect(dr2);

            // Assert
            Assert.AreEqual(expected, intersection);
        }
    }
}
