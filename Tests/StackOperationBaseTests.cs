using System;
using DateRanges;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class StackOperationBaseTests
    {
        [TestMethod]
        public void StackOperationBase_Invoke_EmptyEmpty()
        {
            // Arrange
            var empty1 = new CarPart("Tire", DateRange.Empty());
            var empty2 = new CarPart("Tire", DateRange.Empty());

            // Act
            var result = new CarPartStacker().Invoke(empty1, empty2);

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void StackOperationBase_Invoke_OneEmpty()
        {
            // Arrange
            var empty = new CarPart("Door", DateRange.Empty());
            var dr0101_1201 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));
            var tire = new CarPart("Tire", dr0101_1201);

            // Act
            var result = new CarPartStacker().Invoke(empty, tire);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x => 
                x.DateRange == dr0101_1201 && 
                x.Count() == 1 &&
                x.Contains(tire)
                ));
        }

        [TestMethod]
        public void StackOperationBase_Invoke_NonOverlapping()
        {
            // Scenario:
            // a: |-------|
            // b:           |------------|
            // r: |-------| |------------|

            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var door = new CarPart("Door", dr0101_0301);
            var dr0601_0901 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));
            var tire = new CarPart("Tire", dr0601_0901);

            // Act
            var result = new CarPartStacker().Invoke(door, tire);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0101_0301 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0601_0901 &&
                x.Count() == 1 &&
                x.Contains(tire)
                ));
        }

        [TestMethod]
        public void StackOperationBase_Invoke_Contiguous()
        {
            // Scenario:
            // a: |-------|
            // b:         |------------|
            // r: |-------|------------|

            // Arrange
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var door = new CarPart("Door", dr0101_0301);
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var tire = new CarPart("Tire", dr0301_0601);

            // Act
            var result = new CarPartStacker().Invoke(door, tire);

            // Assert
            Assert.AreEqual(2, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0101_0301 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0301_0601 &&
                x.Count() == 1 &&
                x.Contains(tire)
                ));
        }

        [TestMethod]
        public void StackOperationBase_Invoke_FirstContainsSecond()
        {
            // Scenario:
            // a: |------------------|
            // b:    |------------|
            // r: |--|------------|--|

            // Arrange
            var dr0101_1201 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 12, 1));
            var door = new CarPart("Door", dr0101_1201);
            var dr0301_0601 = new DateRange(Date.NewDate(2019, 3, 1), Date.NewDate(2019, 6, 1));
            var tire = new CarPart("Tire", dr0301_0601);
            // The following are used for the results.
            var dr0101_0301 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 3, 1));
            var dr0601_1201 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));

            // Act
            var result = new CarPartStacker().Invoke(door, tire);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0101_0301 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0301_0601 &&
                x.Count() == 2 &&
                x.Contains(door) &&
                x.Contains(tire)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0601_1201 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
        }

        [TestMethod]
        public void StackOperationBase_Invoke_Overlapping()
        {
            // Scenario:
            // a: |-------------|
            // b:         |------------|
            // r: |-------|-----|------|

            // Arrange
            var dr0101_0901 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 9, 1));
            var door = new CarPart("Door", dr0101_0901);
            var dr0601_1201 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 12, 1));
            var tire = new CarPart("Tire", dr0601_1201);
            // The following are used for the results.
            var dr0101_0601 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 6, 1));
            var dr0601_0901 = new DateRange(Date.NewDate(2019, 6, 1), Date.NewDate(2019, 9, 1));
            var dr0901_1201 = new DateRange(Date.NewDate(2019, 9, 1), Date.NewDate(2019, 12, 1));

            // Act
            var result = new CarPartStacker().Invoke(door, tire);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0101_0601 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0601_0901 &&
                x.Count() == 2 &&
                x.Contains(door) &&
                x.Contains(tire)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0901_1201 &&
                x.Count() == 1 &&
                x.Contains(door)
                ));
        }

        [TestMethod]
        public void StackOperationBase_Invoke_Complex()
        {
            // Scenario:
            // Paint:  |---Red------|-----Blue---------------|
            // Tires:  |------GoodYear------|----Firestone---|
            // Result: |------------|-------|----------------|

            // Arrange
            var dr0101_0401 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 4, 1));
            var red = new CarPart("Red", dr0101_0401);
            var dr0401_1201 = new DateRange(Date.NewDate(2019, 4, 1), Date.NewDate(2019, 12, 1));
            var blue = new CarPart("Blue", dr0401_1201);

            var dr0101_0701 = new DateRange(Date.NewDate(2019, 1, 1), Date.NewDate(2019, 7, 1));
            var goodyear = new CarPart("GoodYear", dr0101_0701);
            var dr0701_1201 = new DateRange(Date.NewDate(2019, 7, 1), Date.NewDate(2019, 12, 1));
            var firestone = new CarPart("Tire", dr0701_1201);
            
            // The following are used for the results.
            var dr0401_0701 = new DateRange(Date.NewDate(2019, 4, 1), Date.NewDate(2019, 7, 1));

            // Act
            var result = new CarPartStacker().Invoke(red, blue, goodyear, firestone);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0101_0401 &&
                x.Count() == 2 &&
                x.Contains(red) &&
                x.Contains(goodyear)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0401_0701 &&
                x.Count() == 2 &&
                x.Contains(blue) &&
                x.Contains(goodyear)
                ));
            Assert.IsNotNull(result.FirstOrDefault(x =>
                x.DateRange == dr0701_1201 &&
                x.Count() == 2 &&
                x.Contains(blue) &&
                x.Contains(firestone)
                ));
        }

        private class CarPart
        {
            public string Description { get; private set; }

            public DateRange DateRange { get; private set; }

            public CarPart(string description, DateRange dateRange)
            {
                Description = description;
                DateRange = dateRange;
            }
        }

        private class CarPartStacker : StackOperationBase<CarPart>
        {
            public CarPartStacker()
            {
            }

            protected override DateRange DateRangeForItem(CarPart item) => item.DateRange;
        }
    }
}
