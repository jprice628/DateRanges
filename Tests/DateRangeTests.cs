using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateRanges;

namespace Tests
{
    [TestClass]
    public class DateRangeTests
    {
        [TestMethod]
        public void DateRange_Ctor_InstantiatesNewDateRange()
        {
            // Arrange
            var dt1 = new DateTime(2019, 1, 1);
            var dt2 = new DateTime(2019, 6, 1);

            // Act
            var dr = new DateRange(dt1, dt2);

            // Assert
            Assert.AreEqual(dt1, dr.StartDate);
            Assert.AreEqual(dt2, dr.EndDate);
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
            var dt1 = new DateTime(2019, 6, 1);
            var dt2 = new DateTime(2019, 1, 1);

            // Act
            new DateRange(dt1, dt2);
        }

        [TestMethod]
        public void DateRange_Equals_ReturnsTrueWhenEqual()
        {
            // Arrange
            var dt1 = new DateTime(2019, 1, 1);
            var dt2 = new DateTime(2019, 6, 1);

            var dr1 = new DateRange(dt1, dt2);
            var dr2 = new DateRange(dt1, dt2);

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
            var dt1 = new DateTime(2019, 1, 1);
            var dt2 = new DateTime(2019, 6, 1);

            var dr1 = new DateRange(dt1, dt2);
            var dr2 = new DateRange(dt1, dt2);

            // Act
            var areEqual = dr1 == dr2;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DateRange_NotEqualsOperator_ReturnsTrueWhenEqual()
        {
            // Arrange
            var dr1 = new DateRange(new DateTime(2019, 1, 1), new DateTime(2019, 6, 1));
            var dr2 = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 3, 1));

            // Act
            var areEqual = dr1 != dr2;

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void DateRange_ToString_ReturnsString()
        {
            // Arrange
            var dr1 = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 10, 15));

            // Act
            var str = dr1.ToString();

            // Assert
            Assert.AreEqual("{\"StartDate\":\"2019-02-10\",\"EndDate\":\"2019-10-15\"}", str);
        }

        [TestMethod]
        public void DateRange_IsEmpty_ReturnsTrueWhenEmpty()
        {
            // Arrange
            var dr = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 2, 10));

            // Act
            var isEmpty = dr.IsEmpty();

            // Assert
            Assert.IsTrue(isEmpty);
        }

        [TestMethod]
        public void DateRange_IsEmpty_ReturnsFalseWhenNotEmpty()
        {
            // Arrange
            var dr = new DateRange(new DateTime(2019, 2, 10), new DateTime(2019, 2, 20));

            // Act
            var isEmpty = dr.IsEmpty();

            // Assert
            Assert.IsFalse(isEmpty);
        }

        [TestMethod]
        public void DateRange_Length_ReturnsLengthOfDateRange()
        {
            // Arrange
            var dr = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var length = dr.Length();

            // Assert
            Assert.AreEqual(new TimeSpan(9, 0, 0, 0), length);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsTrueWhenDateWithinDateRange()
        {
            // Arrange            
            var dr = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr.Contains(new DateTime(2019, 2, 5));

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsFalseWhenDateOutsideDateRange()
        {
            // Arrange            
            var dr = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr.Contains(new DateTime(2019, 2, 25));

            // Assert
            Assert.IsFalse(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsTrueWhenDateIsStartDate()
        {
            // Arrange            
            var dr = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr.Contains(new DateTime(2019, 2, 1));

            // Assert
            Assert.IsTrue(contains);
        }

        [TestMethod]
        public void DateRange_Contains_ReturnsFalseWhenDateIsEndDate()
        {
            // Arrange            
            var dr = new DateRange(new DateTime(2019, 2, 1), new DateTime(2019, 2, 10));

            // Act
            var contains = dr.Contains(new DateTime(2019, 2, 10));

            // Assert
            Assert.IsFalse(contains);
        }
    }
}
