# DateRanges

Provides a structure and a set of operations that can be performed on date ranges.

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