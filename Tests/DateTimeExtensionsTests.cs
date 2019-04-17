using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DateRanges;

namespace Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void DateTimeExtensions_ToDate_ReturnsDate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 2, 14, 10, 53, 36, DateTimeKind.Local);

            // Act
            var date = dateTime.ToDate();

            // Assert
            Assert.AreEqual(
                new DateTime(2019, 2, 14, 0, 0, 0, DateTimeKind.Unspecified),
                date
                );
        }

        [TestMethod]
        public void DateTimeExtensions_ToDateString_ReturnsString()
        {
            // Arrange
            var dateTime = new DateTime(2019, 2, 14, 10, 53, 36, DateTimeKind.Local);

            // Act
            var str = dateTime.ToYMDString();

            // Assert
            Assert.AreEqual("2019-02-14", str);
        }
    }
}
