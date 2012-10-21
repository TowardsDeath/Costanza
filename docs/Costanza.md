Features of Costanza
=======

- [ConversionTool](#conversiontool)
    - ToDecimal
    - ToNullableDecimal
    - ToInteger
    - ToNullableInteger
- [DateTimeTool](#datetimetool)
    - IsAtLeastAYearAgo
    - IsAtLeastAYearFromNow
    - IsNowInRange
    - IsToday
    - SpansMultipleDays
    - SpansMultipleMonths
- [ExtensionsForString](#extensionsforstring)
    - FormatWith
    - IsBlank
    - HasValue
    - EmptyToNull
- [TransactionTool](#transactiontool)
    - CreateTransactionScope


ConversionTool
-------
Conversion from `string` and `object` to integers and decimals usually requires a lot of boilerplate code. You have to check for `null` values, be aware of exceptions et cetera. This usually results in multiple lines of code, which decreases readability and developer happiness. 

The solution to this is the `ConversionTool` class. It provides conversion methods that free you from worries about exceptions and `null` values, all with one line of code. This is particularly helpful in web scenarios where you have to convert `ViewBag` properties or read querystring parameters.

### Converting to a decimal

`ToDecimal` has 3 main arguments.
- `toConvert` the string or object you want to convert to a decimal. Can be `null`.
- `toReturnIfConversionFails` the value that will be returned if the conversion cannot be performed. Default: `0`.
- `provider` specifies the culture-specific formatting rules used to perform the conversion. Default: `CultureInfo.InvariantCulture`.

There are several overloads available to allow you to change the values of these arguments.

```C#
object theDecimal = 0.5m;
string theString = "This won't convert";
string theDecimalInAString = "2,55";
object theNull = null;

ConversionTool.ToDecimal( theDecimal ); // returns 0.5
ConversionTool.ToDecimal( theString ); // returns 0

ConversionTool.ToDecimal( theDecimal, -1 ); // returns 0.5
ConversionTool.ToDecimal( theString, -1 ); // returns -1

/* Dutch culture, which uses a comma as a separator. */
var culture = new CultureInfo( "nl-NL" );
ConversionTool.ToDecimal( theDecimalInAString, culture ); // returns 2.55

ConversionTool.ToDecimal( theNull, -1 ); // returns -1
```

### Converting to a nullable decimal

`ToNullableDecimal` is almost the same as `ToDecimal`. The only difference occurs in case of a failed conversion: instead of a default value, it returns `null` wrapped in a `decimal?`.

`ToNullableDecimal` has 2 main arguments.
- `toConvert` the string or object you want to convert to a nullable decimal. Can be `null`.
- `provider` specifies the culture-specific formatting rules used to perform the conversion. Default: `CultureInfo.InvariantCulture`.

```C#
object theDecimal = 0.5m;
string theString = "This won't convert";
string theDecimalInAString = "2,55";
object theNull = null;

ConversionTool.ToNullableDecimal( theDecimal ); // returns 0.5
ConversionTool.ToNullableDecimal( theString ); // returns (decimal?)null

/* Dutch culture, which uses a comma as a separator. */
var culture = new CultureInfo( "nl-NL" );
ConversionTool.ToNullableDecimal( theDecimalInAString, culture ); // returns 2.55

ConversionTool.ToNullableDecimal( theNull ); // returns (decimal?)null
```

### Converting to an integer

`ToInteger` has 3 main arguments.
- `toConvert` the string or object you want to convert to an integer. Can be `null`.
- `toReturnIfConversionFails` the value that will be returned if the conversion cannot be performed. Default: `0`.
- `provider` specifies the culture-specific formatting rules used to perform the conversion. Default: `CultureInfo.InvariantCulture`.

There are several overloads available to allow you to change the values of these arguments.

```C#
object theInt = 73;
string theString = "This won't convert";
string theIntInAString = "11";
object theNull = null;

ConversionTool.ToInteger( theInt ); // returns 73
ConversionTool.ToInteger( theString ); // returns 0

ConversionTool.ToInteger( theInt, -1 ); // returns 73
ConversionTool.ToInteger( theString, -1 ); // returns -1

/* Dutch culture, which uses a comma as a separator. */
var culture = new CultureInfo( "nl-NL" );
ConversionTool.ToInteger( theIntInAString, culture ); // returns 11

ConversionTool.ToInteger( theNull, -1 ); // returns -1
```

### Converting to a nullable integer

`ToNullableInteger` is almost the same as `ToInteger`. The only difference occurs in case of a failed conversion: instead of a default value, it returns `null` wrapped in an `int?`.

`ToNullableInteger` has 2 main arguments.
- `toConvert` the string or object you want to convert to a nullable integer. Can be `null`.
- `provider` specifies the culture-specific formatting rules used to perform the conversion. Default: `CultureInfo.InvariantCulture`.

```C#
object theInt = 73;
string theString = "This won't convert";
string theIntInAString = "11";
object theNull = null;

ConversionTool.ToNullableInteger( theInt ); // returns 73
ConversionTool.ToNullableInteger( theString ); // returns (int?)null

/* Dutch culture, which uses a comma as a separator. */
var culture = new CultureInfo( "nl-NL" );
ConversionTool.ToNullableInteger( theIntInAString, culture ); // returns 11

ConversionTool.ToNullableInteger( theNull ); // returns (int?)null
```


DateTimeTool
-------
Calculations with dates can be verbose, and a pain if you have to do the same calculations in several places in your code. The `DateTimeTool` contains a few of those calculations that I found myself repeating.

### IsAtLeastAYearAgo

`IsAtLeastAYearAgo` determines whether the supplied date is at least one year from the current datetime.

```C#
var date1 = new DateTimeOffset( new DateTime( 2011, 9, 1, 0, 0, 0 ) ); // 1 september 2011
var date2 = new DateTimeOffset( new DateTime( 2011, 7, 1, 0, 0, 0 ) ); // 1 july 2011

/* For the example's sake, suppose the current date is 1 August 2012 */
DateTimeTool.IsAtLeastAYearAgo( date1 ); // Returns false
DateTimeTool.IsAtLeastAYearAgo( date2 ); // Returns true
```

### IsAtLeastAYearFromNow

`IsAtLeastAYearFromNow` works exactly the same as `IsAtLeastAYearAgo`, except it can be used for dates in the future.

```C#
var date1 = new DateTimeOffset( new DateTime( 2013, 9, 1, 0, 0, 0 ) ); // 1 september 2011
var date2 = new DateTimeOffset( new DateTime( 2013, 7, 1, 0, 0, 0 ) ); // 1 july 2011

/* For the example's sake, suppose the current date is 1 August 2012 */
DateTimeTool.IsAtLeastAYearFromNow( date1 ); // Returns true
DateTimeTool.IsAtLeastAYearFromNow( date2 ); // Returns false
```

### IsNowInRange

`IsNowInRange` determines whether the current datetime falls in a date range.

```C#
var start = DateTimeOffset.UtcNow.AddDays( -1 );
var end = DateTimeOffset.UtcNow.AddDays( 1 );

DateTimeTool.IsNowInRange( start, end ); // Returns true
```

### IsToday

`IsToday` determines whether the supplied datetime has the same date component as the current datetime.

```C#
var now = DateTimeOffset.UtcNow;
var tomorrow = DateTimeOffset.UtcNow.AddDays( 1 );

DateTimeTool.IsToday( now ); // Returns true
DateTimeTool.IsToday( tomorrow ); // Returns false
```

### SpansMultipleDays

`SpansMultipleDays` determines whether the start of a date range falls on another day than the end of that date range.

```C#
var differentStartDay = new DateTimeOffset( new DateTime( 2012, 12, 31, 23, 0, 0 ) ); // 31 December 2012 23:00
var differentEndDay = new DateTimeOffset( new DateTime( 2013, 1, 1, 1, 0, 0 ) ); // 1 January 2013 1:00

DateTimeTool.SpansMultipleDays( differentStartDay, differentEndDay ); // Returns true


var sameDayStart = new DateTimeOffset( new DateTime( 2012, 8, 8, 17, 0, 0 ) ); // 8 August 17:00
var sameDayEnd = new DateTimeOffset( new DateTime( 2012, 8, 8, 23, 0, 0 ) ); // 8 August 23:00

DateTimeTool.SpansMultipleDays( sameDayStart, sameDayEnd ); // Returns false
```

### SpansMultipleMonths

`SpansMultipleMonths` determines whether the start of a date range falls in another month than the end of that date range, similar to the `SpansMultipleDays` method.

```C#
var differentMonthStart = new DateTimeOffset( new DateTime( 2012, 1, 1 ) ); // 1 January 2012
var differentMonthEnd = new DateTimeOffset( new DateTime( 2012, 3, 1 ) ); // 1 March 2012

DateTimeTool.SpansMultipleMonths( differentMonthStart, differentMonthEnd ); // Returns true

var sameMonthStart = new DateTimeOffset( new DateTime( 2012, 1, 1 ) ); // 1 January 2012
var sameMonthEnd = new DateTimeOffset( new DateTime( 2012, 1, 15 ) ); // 15 January 2012

DateTimeTool.SpansMultipleMonths( sameMonthStart, sameMonthEnd ); // Returns false
```


ExtensionsForString
-------
These are extension methods that are deemed useful enough to be present on every instance of `string`.

### FormatWith

`FormatWith` is a simple wrapper around the default `String.Format()`. It provides a more intuitive way of typing when you're formatting strings.

```C#
string format = "{0} bottles of Bosco";
string s1 = String.Format( format, 4 ); // Standard formatting with .NET
string s2 = format.FormatWith( 4 ); // Costanza extension method: much more intuitive to type
```

You can also specify a format provider if you want to have control over culture-specific settings.
```C#
var culture = new CultureInfo( "nl-NL" );
string s1 = "{0} points".FormatWith( culture, 2.5m ); // s1 is "2,5 points"
```

### IsBlank

`IsBlank` is a simple wrapper around `String.IsNullOrWhiteSpace()`. Like `FormatWith`, this enables a more intuitive way of checking whether a string has contents. It returns `true` if the string is null or consists of whitespace. Otherwise, returns `false`.

```C#
string someVariable = "Not empty";

// Standard check with .NET
String.IsNullOrWhiteSpace( someVariable ); // returns false

// Costanza extension method: much more intuitive to type
someVariable.IsBlank(); // returns false
```

### HasValue

`HasValue` is the exact opposite of `IsBlank`: it returns `false` if the string is null or consists of whitespace. Otherwise, returns `true`.

```C#
string someVariable = "Not empty";

// Standard check with .NET
!String.IsNullOrWhiteSpace( someVariable ); // returns true

// Costanza extension method: much more intuitive to type
someVariable.HasValue(); // returns true
```

### EmptyToNull

There are situations where you want to eliminate whitespace and want a string to have contents, or be null. You can use `EmptyToNull` for this: it turns whitespace strings into null values.

```C#
string nullString = null;
string empty = "";
string contents = "Not empty";

nullString.EmptyToNull(); // returns null
empty.EmptyToNull(); // returns null
contents.EmptyToNull(); // returns "Not empty";
```


TransactionTool
-------
You would be very wise to use transactions in your code. The `TransactionScope` class in .NET makes this very easy. However, the default constructor sets some very inconvenient and even harmful defaults. 

- **The transaction timeout is set to 1 minute.** If you change the timeout of your `SqlCommand` to more than 1 minute, you probably don't expect your transaction to timeout before that.
- **The transaction isolation level is set to Serializable.** This is a setting that's prone to deadlocks. ReadCommitted is a better option in this case.

The solution to this is the `TransactionTool` class. It creates a new `TransactionScope` without the harmful defaults.

```C#
using( var transaction = TransactionTool.CreateTransactionScope() )
{
    // Use the transaction.
}
```

Credits: the solution to this problem was found in this article on David Browne's weblog: [Using new TransactionScope() Considered Harmful](http://blogs.msdn.com/b/dbrowne/archive/2010/06/03/using-new-transactionscope-considered-harmful.aspx). The code in that article is used in the `TransacionTool` class.