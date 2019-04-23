using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
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
            Assert.AreEqual(new DateTime(2019, 2, 14, 0, 0, 0),date);
            // The DateTime.Equals method doesn't check to see if the Kind 
            // property of the two values are equal.
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);
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
