using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests
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

        [TestMethod]
        public void Date_IsDate_ReturnsTrueWhenDate()
        {
            // Arrange
            var date = Date.NewDate(2019, 2, 14);

            // Act
            var isDate = Date.IsDate(date);

            // Assert
            Assert.IsTrue(isDate);
        }

        [TestMethod]
        public void Date_IsDate_ReturnsFalseWhenTimeComponentsExist()
        {
            // Arrange
            var date = new DateTime(2019, 2, 14, 10, 52, 32, DateTimeKind.Unspecified);

            // Act
            var isDate = Date.IsDate(date);

            // Assert
            Assert.IsFalse(isDate);
        }

        [TestMethod]
        public void Date_IsDate_ReturnsFalseWhenKindNotUnspecified()
        {  
            // Arrange
            var date = new DateTime(2019, 2, 14, 0, 0, 0, DateTimeKind.Local);

            // Act
            var isDate = Date.IsDate(date);

            // Assert
            Assert.IsFalse(isDate);
        }

        [TestMethod]
        public void Date_AreEqual_ReturnsTrue()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 2, 14);
            var date2 = Date.NewDate(2019, 2, 14);

            // Act
            var areEqual = Date.AreEqual(date1, date2);

            // Assert
            Assert.IsTrue(areEqual);
        }

        [TestMethod]
        public void Date_AreEqual_ReturnsFalse()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 2, 14);
            var date2 = Date.NewDate(2019, 3, 14);

            // Act
            var areEqual = Date.AreEqual(date1, date2);

            // Assert
            Assert.IsFalse(areEqual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_AreEqual_ThrowsWhenNotADate()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 2, 14);
            var date2 = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.AreEqual(date1, date2);
        }

        [TestMethod]
        public void Date_IsMaxValue_ReturnsTrue()
        {
            // Arrange
            var date = Date.MaxValue;

            // Act
            var isMaxValue = Date.IsMaxValue(date);

            // Assert
            Assert.IsTrue(isMaxValue);
        }

        [TestMethod]
        public void Date_IsMaxValue_ReturnsFalse()
        {
            // Arrange
            var date = Date.NewDate(2019, 2, 14);

            // Act
            var isMaxValue = Date.IsMaxValue(date);

            // Assert
            Assert.IsFalse(isMaxValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_IsMaxValue_ThrowsWhenNotADate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.IsMaxValue(dateTime);
        }

        [TestMethod]
        public void Date_IsMinValue_ReturnsTrue()
        {
            // Arrange
            var date = Date.MinValue;

            // Act
            var isMinValue = Date.IsMinValue(date);

            // Assert
            Assert.IsTrue(isMinValue);
        }

        [TestMethod]
        public void Date_IsMinValue_ReturnsFalse()
        {
            // Arrange
            var date = Date.NewDate(2019, 2, 14);

            // Act
            var isMinValue = Date.IsMinValue(date);

            // Assert
            Assert.IsFalse(isMinValue);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_IsMinValue_ThrowsWhenNotADate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.IsMinValue(dateTime);
        }

        [TestMethod]
        public void Date_IsToday_ReturnsTrue()
        {
            // Arrange
            var date = Date.Today;

            // Act
            var isToday = Date.IsToday(date);

            // Assert
            Assert.IsTrue(isToday);
        }

        [TestMethod]
        public void Date_IsToday_ReturnsFalse()
        {
            // Arrange
            var date = Date.NewDate(2019, 2, 14);

            // Act
            var isToday = Date.IsToday(date);

            // Assert
            Assert.IsFalse(isToday);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_IsToday_ThrowsWhenNotADate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.IsToday(dateTime);
        }

        [TestMethod]
        public void Date_Min_ReturnsFirstValue()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 2, 14);
            var date2 = Date.NewDate(2019, 3, 14);

            // Act
            var min = Date.Min(date1, date2);

            // Assert
            Assert.AreEqual(date1, min);
        }

        [TestMethod]
        public void Date_Min_ReturnsSecondValue()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 3, 14);
            var date2 = Date.NewDate(2019, 2, 14);

            // Act
            var min = Date.Min(date1, date2);

            // Assert
            Assert.AreEqual(date2, min);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_Min_ThrowsWhenNotADate()
        {
            // Arrange
            var date = Date.NewDate(2019, 3, 14);
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.Min(date, dateTime);
        }

        [TestMethod]
        public void Date_Max_ReturnsFirstValue()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 3, 14);
            var date2 = Date.NewDate(2019, 2, 14);

            // Act
            var max = Date.Max(date1, date2);

            // Assert
            Assert.AreEqual(date1, max);
        }

        [TestMethod]
        public void Date_Max_ReturnsSecondValue()
        {
            // Arrange
            var date1 = Date.NewDate(2019, 2, 14);
            var date2 = Date.NewDate(2019, 3, 14);

            // Act
            var max = Date.Max(date1, date2);

            // Assert
            Assert.AreEqual(date2, max);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_Max_ThrowsWhenNotADate()
        {
            // Arrange
            var date = Date.NewDate(2019, 3, 14);
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);

            // Act
            Date.Max(date, dateTime);
        }

        [TestMethod]
        public void Date_Clamp_ReturnsDate()
        {
            // Arrange
            var date = Date.NewDate(2019, 2, 14);
            var minDate = Date.NewDate(2019, 1, 1);
            var maxDate = Date.NewDate(2019, 12, 31);

            // Act
            var clampedDate = Date.Clamp(date, minDate, maxDate);

            // Assert
            Assert.AreEqual(date, clampedDate);
        }

        [TestMethod]
        public void Date_Clamp_ReturnsMin()
        {
            // Arrange
            var date = Date.NewDate(2018, 2, 14);
            var minDate = Date.NewDate(2019, 1, 1);
            var maxDate = Date.NewDate(2019, 12, 31);

            // Act
            var clampedDate = Date.Clamp(date, minDate, maxDate);

            // Assert
            Assert.AreEqual(minDate, clampedDate);
        }

        [TestMethod]
        public void Date_Clamp_ReturnsMax()
        {
            // Arrange
            var date = Date.NewDate(2020, 2, 14);
            var minDate = Date.NewDate(2019, 1, 1);
            var maxDate = Date.NewDate(2019, 12, 31);

            // Act
            var clampedDate = Date.Clamp(date, minDate, maxDate);

            // Assert
            Assert.AreEqual(maxDate, clampedDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Date_Clamp_ThrowsWhenNotADate()
        {
            // Arrange
            var dateTime = new DateTime(2019, 3, 14, 10, 52, 14, DateTimeKind.Local);
            var minDate = Date.NewDate(2019, 1, 1);
            var maxDate = Date.NewDate(2019, 12, 31);

            // Act
            Date.Clamp(dateTime, minDate, maxDate);
        }
    }
}
