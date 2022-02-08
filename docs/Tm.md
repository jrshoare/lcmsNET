# Tm Struct

Namespace: lcmsNET  
Assembly: lcmsNET.dll

Represents a calendar date and time broken into its components.

```csharp
public struct Tm
```

Inheritance Object → ValueType → Tm

## Fields

|  |  |  |
| ----- | --- | --- |
| sec   | int | Seconds after the minute in the range 0..61. |
| min   | int | Minutes after the hour in the range 0..59. |
| hour  | int | Hours since midnight in the range 0..23. |
| mday  | int | Day of the month in the range 1..31. |
| mon   | int | Month of the year in the range 0..11. |
| year  | int | Years since 1900. |
| wday  | int | Days since Sunday in the range 0..6. |
| yday  | int | Days since January 1 in the range 0..365. |
| isdst | int | Daylight saving time flag. Greater than zero if Daylight Savings is in effect, zero if not in effect or less than zero if information is not available. |

## Constructors
## Tm(DateTime) Constructor

```csharp
public Tm(DateTime date)
```

Create a new instance of the `Tm` structure.

### Parameters

`date` DateTime  
A `DateTime` instance.

## Methods
## implicit operator DateTime(Tm) Method

```csharp
public static implicit operator DateTime(Tm tm)
```

Implicitly converts a `Tm` to a `DateTime`.

### Parameters

`tm` [Tm](./Tm.md)  
The `Tm` to be converted.

### Returns

`DateTime`  
The calendar date and time as a `DateTime`.