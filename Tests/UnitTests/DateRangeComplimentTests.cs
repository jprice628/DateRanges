using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateRanges;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DateRangeComplimentTests
    {
        [TestMethod]
        public void DateRange_Compliment_SingleDateRange()
        {
            // Arrange
            var dr0101_1201 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));
            var drMin_0101 = new DateRange(Date.MinValue, Date.NewDate(2019, 1, 1));
            var dr1201_Max = new DateRange(Date.NewDate(2019, 12, 1), Date.MaxValue);

            // Act
            var result = dr0101_1201.Compliment();

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.Contains(drMin_0101));
            Assert.IsTrue(result.Contains(dr1201_Max));
        }

        [TestMethod]
        public void DateRange_Compliment_MultipleDateRanges()
        {
            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr0601_1201 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var drMin_0101 = new DateRange(Date.MinValue, Date.NewDate(2019, 1, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var dr1201_Max = new DateRange(Date.NewDate(2019, 12, 1), Date.MaxValue);

            var set = new[]
            {
                dr0101_0301,
                dr0601_1201
            };

            // Act
            var result = DateRange.Compliment(set);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(drMin_0101));
            Assert.IsTrue(result.Contains(dr0301_0601));
            Assert.IsTrue(result.Contains(dr1201_Max));
        }

        [TestMethod]
        public void DateRange_Compliment_MultipleDateRangesParams()
        {
            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr0601_1201 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var drMin_0101 = new DateRange(Date.MinValue, Date.NewDate(2019, 1, 1));
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var dr1201_Max = new DateRange(Date.NewDate(2019, 12, 1), Date.MaxValue);

            // Act
            var result = DateRange.Compliment(dr0101_0301, dr0601_1201);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Contains(drMin_0101));
            Assert.IsTrue(result.Contains(dr0301_0601));
            Assert.IsTrue(result.Contains(dr1201_Max));
        }
    }
}
