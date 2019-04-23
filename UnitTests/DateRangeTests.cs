using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
{
    [TestClass]
    public class DateRangeTests
    {
        [TestMethod]
        public void DateRange_Ctor_InstantiatesNewDateRange()
        {
            // Arrange
            var dt0101 = new DateTime(2019, 1, 1);
            var dt0601 = new DateTime(2019, 6, 1);

            // Act
            var result = new DateRange(dt0101, dt0601);

            // Assert
            Assert.AreEqual(dt0101, result.StartDate);
            Assert.AreEqual(dt0601, result.EndDate);
        }

        [TestMethod]
        public void DateRange_Full_InstantiatesFullDateRange()
        {
            // Act
            var full = DateRange.Full();

            // Assert
            Assert.AreEqual(
                new DateRange(DateTime.MinValue, DateTime.MaxValue),
                full
                );
        }

        [TestMethod]
        public void DateRange_Empty_InstantiatesEmptyDateRange()
        {
            // Act
            var empty = DateRange.Empty();

            // Assert
            Assert.IsTrue(empty.IsEmpty());
        }

        [TestMethod]
        public void DateRange_Ctor_StripsTimeInfo()
        {
            // Act
            var dr = new DateRange(
                new DateTime(2019, 1, 1, 10, 34, 23, DateTimeKind.Local),
                new DateTime(2019, 6, 1, 15, 56, 32, DateTimeKind.Utc)
                );

            // Assert
            Assert.AreEqual(
                new DateTime(2019, 1, 1, 0, 0, 0, DateTimeKind.Unspecified),
                dr.StartDate
                );
            Assert.AreEqual(
                new DateTime(2019, 6, 1, 0, 0, 0, DateTimeKind.Unspecified),
                dr.EndDate
                );
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void DateRange_Ctor_ThrowsWhenStartGreaterThanEnd()
        {
            // Arrange
            var dt0601 = new DateTime(2019, 6, 1);
            var dt0101 = new DateTime(2019, 1, 1);

            // Act
            new DateRange(dt0601, dt0101);
        }

        [TestMethod]
        public void DateRange_Equals_ReturnsTrueWhenEqual()
        {
            // Arrange
            var dt0101 = new DateTime(2019, 1, 1);
            var dt0601 = new DateTime(2019, 6, 1);

            var dr1 = new DateRange(dt0101, dt0601);
            var dr2 = new DateRange(dt0101, dt0601);

            // Act
            var areEqual = dr1.Equals(dr2);

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DateRange_Equals_ReturnsFalseWhenNotEqual()
        {
            // Arrange
            var dr1 = new DateRange(new DateTime(2019, 1, 1), new DateTime(2019, 6, 1));
            var dr2 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 3, 1));

            // Act
            var areEqual = dr1.Equals(dr2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        public void DateRange_EqualsOperator_ReturnsTrueWhenEqual()
        {
            // Arrange
            var dt0101 = new DateTime(2019, 1, 1);
            var dt0601 = new DateTime(2019, 6, 1);

            var dr1 = new DateRange(dt0101, dt0601);
            var dr2 = new DateRange(dt0101, dt0601);

            // Act
            var areEqual = dr1 == dr2;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DateRange_NotEqualsOperator_ReturnsTrueWhenEqual()
        {
            // Arrange
            var dr0101_0601 = new DateRange(new DateTime(2019, 1, 1), new DateTime(2019, 6, 1));
            var dr0201_0301 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 3, 1));

            // Act
            var areEqual = dr0101_0601 != dr0201_0301;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DateRange_ToString_ReturnsString()
        {
            // Arrange
            var dr0210_1015 = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 10, 15));

            // Act
            var str = dr0210_1015.ToString();

            // Assert
            Assert.AreEqual("2019-02-10 to 2019-10-15", str);
        }

        [TestMethod]
        public void DateRange_IsEmpty_ReturnsTrueWhenEmpty()
        {
            // Arrange
            var dr0210_0210 = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 2, 10));

            // Act
            var isEmpty = dr0210_0210.IsEmpty();

            // Assert
            Assert.IsTrue(isEmpty);
        }

        [TestMethod]
        public void DateRange_IsEmpty_ReturnsFalseWhenNotEmpty()
        {
            // Arrange
            var dr0210_0220 = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 2, 20));

            // Act
            var isEmpty = dr0210_0220.IsEmpty();

            // Assert
            Assert.IsFalse(isEmpty);
        }

        [TestMethod]
        public void DateRange_Length_ReturnsLengthOfDateRange()
        {
            // Arrange
            var dr0201_0210 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var length = dr0201_0210.Length();

            // Assert
            Assert.AreEqual(TimeSpan.FromDays(9), length);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsTrueWhenDateWithinDateRange()
        {
            // Arrange            
            var dr0201_0210 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr0201_0210.Contains(new DateTime(2019, 2, 5));

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsFalseWhenDateOutsideDateRange()
        {
            // Arrange            
            var dr0201_0210 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr0201_0210.Contains(new DateTime(2019, 2, 25));

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsTrueWhenDateIsStartDate()
        {
            // Arrange            
            var dr0201_0210 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr0201_0210.Contains(new DateTime(2019, 2, 1));

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsFalseWhenDateIsEndDate()
        {
            // Arrange            
            var dr0201_0210 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr0201_0210.Contains(new DateTime(2019, 2, 10));

            // Assert
            Assert.IsFalse(contains);
        }
    }
}
