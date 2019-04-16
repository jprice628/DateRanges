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
            var dr = new DateRange(dt1, dt2);
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
    }
}
