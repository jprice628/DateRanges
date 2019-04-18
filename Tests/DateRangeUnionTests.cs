using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateRanges;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DateRangeUnionTests
    {
        [TestMethod]
        public void DateRange_Union_ReturnsEmptyWhenBothDr1AndDr2AreEmpty()
        {
            // Arrange
            var dr1 = DateRange.Empty();
            var dr2 = DateRange.Empty();

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(0, union.Count());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsDr2WhenDr1IsEmpty()
        {
            // Arrange
            var dr1 = DateRange.Empty();
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(dr2, union.First());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsDr1WhenDr2IsEmpty()
        {
            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31)); 
            var dr2 = DateRange.Empty();

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(dr1, union.First());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsSetWhenDr1LessThanDr2()
        {
            // Scenario:
            // dr1    |-------|
            // dr2              |------------|
            // Result |-------| |------------|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(2, union.Count());
            Assert.IsTrue(union.Contains(dr1));
            Assert.IsTrue(union.Contains(dr2));
        }

        [TestMethod]
        public void DateRange_Union_ReturnsSetWhenDr2LessThanDr1()
        {
            // Scenario:
            // dr1                |-------|
            // dr2    |---------|
            // Result |---------| |-------|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(2, union.Count());
            Assert.IsTrue(union.Contains(dr1));
            Assert.IsTrue(union.Contains(dr2));
        }

        [TestMethod]
        public void DateRange_Union_ReturnsWhenDr1ContainsDr2()
        {
            // Scenario:
            // dr1    |----------------|
            // dr2       |---------|
            // Result |----------------|

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(dr1, union.First());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsWhenDr2ContainsDr1()
        {
            // Scenario:
            // dr1      |--------|
            // dr2    |-------------|
            // Result |-------------|

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 31));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(dr2, union.First());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsWhenDr1LeftUnionsDr2()
        {
            // Scenario:
            // dr1      |--------|
            // dr2            |---------|
            // Result   |---------------|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var expected = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 9, 1));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(expected, union.First());
        }

        [TestMethod]
        public void DateRange_Union_ReturnsWhenDr1RightUnionsDr2()
        {
            // Scenario:
            // dr1          |--------|
            // dr2      |---------|
            // Result   |------------|
            //
            // Note: StartDates are inclusive. EndDates are exclusive.

            // Arrange
            var dr1 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var dr2 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 9, 1));
            var expected = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 12, 1));

            // Act
            var union = dr1.Union(dr2);

            // Assert
            Assert.AreEqual(1, union.Count());
            Assert.AreEqual(expected, union.First());
        }
    }
}
