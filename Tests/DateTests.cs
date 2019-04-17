using System;
using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class DateTests
    {
        [TestMethod]
        public void Date_NewDateYMD_InstantiatesNewDate()
        {
            // Act
            var date = Date.NewDate(2019, 2, 14);

            // Assert
            Assert.AreEqual(new DateTime(2019, 2, 14), date);
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);
        }

        [TestMethod]
        public void Date_NewDateDT_InstantiatesNewDate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 2, 14, 10, 53, 35, DateTimeKind.Local);
            // Act

            var date = Date.NewDate(dateTime);

            // Assert
            Assert.AreEqual(new DateTime(2019, 2, 14), date);
            Assert.AreEqual(DateTimeKind.Unspecified, date.Kind);
        }
    }
}
