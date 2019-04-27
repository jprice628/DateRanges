# DateRanges Library

Provides a structure and a set of operations that can be performed on date ranges.

There are many use-cases for representing a range of dates and being able to perform basic operations on them. For example: timeline applications, working with contracts that have effective and expiration dates, and aggregating attributes that change over time.

The focus of this library is the DateRange structure which enables us to  represent a contiguous series of dates by identifying a start date and an end date, e.g. 3/14/2019 to 6/23/2019. 

>*NOTE*: Start dates are inclusive while end dates are exclusive, i.e. 3/14/2019 is within the DateRange used as an example above, but 6/23/2019 is not. 

# Dates and Date Features

The .NET framework provides fairly robust support for DateTime values, but the concept of a date without time components doesn't exist. When dealing with date ranges, we don't really want the time components, especially times zones, to become a problem. For example, when using the class property `DateTime.Today`, the .NET framework provides a DateTime value with the date components set to today's date and the time components set to zero. The timezone will be set to local, so the zero-valued time components really mean midnight in the local timezone which can become problematic when performing DateRange operations, especially if the timezones and time components are inconsistent.

```
var today = DateTime.Today;
WriteLine(today.ToString("MM/dd/yyyy HH:mm:ss"));
// 04/26/2019 00:00:00
WriteLine(today.Kind);
// Local
```

To solve this, a set of features has been implemented to constrain the existing DateTime structure and provide common functionality for date values.

## Creating a Date Value

To Ensure that you are working with date values, use one of the overloads of the `Date.NewDate` method.

```
// Example 1 - Creating a date value using an existing DateTime value
var dateTime = new DateTime(2019, 1, 15, 10, 54, 32);
var date1 = Date.NewDate(dateTime);
```

```
// Example 2 - Creating a date value using date components
var date2 = Date.NewDate(2019, 3, 14);
```

```
// Example 3 - Getting common date values
var today = Date.Today;
var min = Date.MinValue;
var max = Date.MaxValue;
```

## Ensuring That a Value is a Date

Use the `IsDate` function to when there is a need to identify whether or not a value is a date.

> *CAUTION*: Many of the other methods associated with date values will throw an exception if a non-date value is provided. This includes the equality and helper methods described in the following sections.

```
// Example 1 - Not a date value
var dateTime = new DateTime(2019, 1, 15, 10, 54, 32);
Date.IsDate(dateTime); // False
```

```
// Example 2 - A valid date value
Date.IsDate(Date.Today)); //True
```


## Testing Equality

Two values are equal when they are both date values and their date components are equal. This can be tested using the `AreEqual` function.

```
// Example 1
var date1 = Date.NewDate(2019, 3, 14);
var date2 = Date.NewDate(2019, 3, 14);
WriteLine(Date.AreEqual(date1, date2)); // True
```

```
// Example 2
var date3 = Date.NewDate(2019, 3, 14);
var date4 = Date.NewDate(2019, 6, 1);
WriteLine(Date.AreEqual(date3, date4)); // False
```

```
// Example 3
// The following will throw an exception. DateTime.Today is 
// not a valid date value because its "Kind" property is "Local".
Date.AreEqual(Date.Today, DateTime.Today);
```

The following helper methods are also provided for testing equality against common values:
- `Date.IsMinValue(someDate)`
- `Date.IsMaxValue(someDate)`
- `Date.IsToday(someDate)`

## Other Helpful Functions

`Date.Min(date1, date2);` can be used to get the lesser of two date values.

`Date.Max(date1, date2);` can be used to get the lesser of two date values.

`Date.Clamp(date, minValue, maxValue)` can be used to constrain a date to a specific range. For example:

```
// Example 1
var value = Date.NewDate(2019, 2, 14);
var min = Date.NewDate(2019, 3, 1);
var max = Date.NewDate(2019, 3, 31);

Date.Clamp(value, min, max); // Returns 3/1/2019.
```

```
// Example 2
var value = Date.NewDate(2019, 3, 15);
var min = Date.NewDate(2019, 3, 1);
var max = Date.NewDate(2019, 3, 31);

Date.Clamp(value, min, max); // Returns 3/15/2019.
```

```
// Example 3
var value = Date.NewDate(2019, 4, 23);
var min = Date.NewDate(2019, 3, 1);
var max = Date.NewDate(2019, 3, 31);

Date.Clamp(value, min, max); // Returns 3/31/2019.
```

# DateRange Operations

## Creating a DateRange Value

A new DateRange value can be created by calling the DateRange constructor. Note that use of Date values is not required, but they are used internally. If a value with time components is passed to the DateRange constructor, it will be converted to a date value, and the time components will be lost.

```
// Creates a new DateRange value.
var dateRange = new DateRange(
    Date.NewDate(2019, 3, 14),
    Date.NewDate(2019, 6, 23));
```

For convinience the following two functions have been provided for creating common DateRange values:
- `DateRange.Full()` - an alias for `new DateRange(Date.MinValue, Date.MaxValue)`
- `DateRange.Empty()` - an alias for `new DateRange(Date.MinValue, Date.MinValue)`

## Length of DateRange Values

Calling `DateRange.Length()` on a DateRange value returns a TimeSpan value.

```
var dateRange = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 1, 10));

dateRange.Length(); // 9 days
```
>*NOTE*: The length of the DateRange value above is 9 days because the end date, 1/10/2019, is not contained within the DateRange. End dates are exclusive.

Calling `DateRange.Empty()` returns true when the length of a DateRange is zero.

```
var dateRange = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 1, 1));

dateRange.IsEmpty(); // True
```

## Testing to See if a DateRange Contains a Date

The `DateRange.Contains(DateTime)` method can be used to see if a given DateRange contains a given date.

```
var dateRange = new DateRange(
    Date.NewDate(2019, 3, 14),
    Date.NewDate(2019, 6, 23));
var date = Date.NewDate(2019, 4, 8);

dateRange.Contains(date); // True
```

# Set Operations

## Unions

Applying the union operation to one or more DateRanges returns a new set of DateRange values that includes all dates from the input set.

```
             1/1     1/5     1/10    1/15   1/20             1/30
Input Set:    |---------------|              |----------------|
              .       |---------------|      .                .
Result Set:   |-----------------------|      |----------------|
```

```
// Example 1 - Union of Two DateRanges
var dr0101_0110 = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 1, 10));
var dr0105_0115 = new DateRange(
    Date.NewDate(2019, 1, 5),
    Date.NewDate(2019, 1, 15));

// Returns 1/1/2019 to 1/15/2019
dr0101_0110.Union(dr0105_0115);
```

```
// Example 2 - Union of a set of DateRanges
var set = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 1),
        Date.NewDate(2019, 1, 10)),
    new DateRange(
        Date.NewDate(2019, 1, 5),
        Date.NewDate(2019, 1, 15)),
    new DateRange(
        Date.NewDate(2019, 2, 1),
        Date.NewDate(2019, 2, 14))
};

// Returns
// 1/1/2019 to 1/15/2019,
// 2/1/2019 to 2/14/2019
DateRange.Union(set);
```

## Intersections

Applying the intersect operation to one or more DateRanges returns a new set of DateRange values that includes the dates that exist in all of the given inputs.

```
             1/1     1/5     1/10    1/15
Input Set:    |---------------|          
                      |---------------|  
Result Set:           |-------|
```

```
// Example 1 - Intersection of Two DateRanges
var dr0101_0110 = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 1, 10));
var dr0105_0115 = new DateRange(
    Date.NewDate(2019, 1, 5),
    Date.NewDate(2019, 1, 15));

// Returns 1/5/2019 to 1/10/2019
dr0101_0110.Intersect(dr0105_0115);
```

The operation can also be performed on sets of DateRange values. For example, imagine a situation where two collegues travel frequently to the same city. They decide that it would be more economical to lease and share an apartment. Each collegue's stays could be represented as a set of DateRange values, and the intersection would represent the dates that they occupied the apartment together.

```
// Example 2 - Intersection of Two DateRange Sets.
var jane = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 1),
        Date.NewDate(2019, 1, 10)),
    new DateRange(
        Date.NewDate(2019, 1, 20),
        Date.NewDate(2019, 2, 1))
};

var sally = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 5),
        Date.NewDate(2019, 1, 9)),
    new DateRange(
        Date.NewDate(2019, 1, 15),
        Date.NewDate(2019, 1, 23))
};

// Returns 
// 1/5/2019 to 1/9/2019,
// 1/20/2019 to 1/23/2019
DateRange.Intersect(jane, sally);
```

## Differences

Applying the difference operation to one or more DateRanges returns a new set of DateRange values that includes the dates that exist in the first DateRange but not in the others.

```
             1/1     1/5     1/10    1/15
Input Set:    |---------------|          
                      |---------------|  
Result Set:   |-------|
```

```
// Example 1 - Difference of Two DateRanges
var dr0101_0110 = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 1, 10));
var dr0105_0115 = new DateRange(
    Date.NewDate(2019, 1, 5),
    Date.NewDate(2019, 1, 15));

// Returns 1/1/2019 to 1/5/2019
dr0101_0110.Difference(dr0105_0115);
```

```
// Example 2 - Difference of a DateRange and a set of DateRanges.
var dr0101_0201 = new DateRange(
    Date.NewDate(2019, 1, 1),
    Date.NewDate(2019, 2, 1));

var dateRanges = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 5),
        Date.NewDate(2019, 1, 10)),
    new DateRange(
        Date.NewDate(2019, 1, 20),
        Date.NewDate(2019, 1, 25)),
};

// Returns
// 2019-01-01 to 2019-01-05,
// 2019-01-10 to 2019-01-20,
// 2019-01-25 to 2019-02-01
dr0101_0110.Difference(dateRanges);
```

The operation can also be performed on sets of DateRange values. Using the previous example where two collegues share an apartment, the difference operation could be used to identify DateRanges where a collegue occupied the apartment by herself.

```
// Example 3 - Difference of Two DateRange Sets.
var jane = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 1),
        Date.NewDate(2019, 1, 10)),
    new DateRange(
        Date.NewDate(2019, 1, 20),
        Date.NewDate(2019, 2, 1))
};

var sally = new[]
{
    new DateRange(
        Date.NewDate(2019, 1, 5),
        Date.NewDate(2019, 1, 9)),
    new DateRange(
        Date.NewDate(2019, 1, 15),
        Date.NewDate(2019, 1, 23))
};

// Returns
// 2019-01-01 to 2019-01-05
// 2019-01-09 to 2019-01-10
// 2019-01-23 to 2019-02-01
DateRange.Difference(jane, sally);
```