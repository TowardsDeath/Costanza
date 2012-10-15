using System;
using System.Globalization;

namespace Costanza
{
    /// <summary>
    /// Provides utility methods for easy converting between datatypes.
    /// </summary>
    public static class ConversionTool
    {
        /// <summary>
        /// Safely tries to convert an object to a decimal, and if the conversion fails, returns 0.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The decimal inside the supplied object, or 0 if the conversion fails.</returns>
        public static decimal ToDecimal( object o )
        {
            return ConversionTool.ToDecimal( o != null ? o.ToString() : "" );
        }

        /// <summary>
        /// Safely tries to convert a string to a decimal, and if the conversion fails, returns 0.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The decimal inside the supplied string, or 0 if the conversion fails.</returns>
        public static decimal ToDecimal( string s )
        {
            return ConversionTool.ToDecimal( s, 0m );
        }

        /// <summary>
        /// Safely tries to convert a string to a decimal, and if the conversion fails, returns a specified value.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="toReturnIfConversionFails">The value that will be returned if the conversion fails.</param>
        /// <returns>
        /// The decimal inside the supplied string, or the value of 
        /// <paramref name="toReturnIfConversionFails"/> if the conversion fails.
        /// </returns>
        public static decimal ToDecimal( string s, decimal toReturnIfConversionFails )
        {
            return ConversionTool.ToDecimal( s, toReturnIfConversionFails, CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Safely tries to convert a string to a decimal using the specified format provider.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The decimal inside the supplied string, or 0 if the conversion fails.</returns>
        public static decimal ToDecimal( string s, IFormatProvider provider )
        {
            return ConversionTool.ToDecimal( s, 0, provider );
        }

        /// <summary>
        /// Safely tries to convert a string to a decimal using the specified format provider, 
        /// and if the conversion fails, returns a specified value.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="toReturnIfConversionFails">The value that will be returned if the conversion fails.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// The decimal inside the supplied string, or the value of 
        /// <paramref name="toReturnIfConversionFails"/> if the conversion fails.
        /// </returns>
        public static decimal ToDecimal( string s, decimal toReturnIfConversionFails, IFormatProvider provider )
        {
            decimal d = 0;
            return Decimal.TryParse( s, NumberStyles.Any, provider, out d ) ? d : toReturnIfConversionFails;
        }

        /// <summary>
        /// Safely tries to convert an object to a nullable decimal.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>
        /// The decimal inside the supplied object, or null wrapped in a nullable decimal if the conversion fails.
        /// </returns>
        public static decimal? ToNullableDecimal( object o )
        {
            return ConversionTool.ToNullableDecimal( o != null ? o.ToString() : "" );
        }

        /// <summary>
        /// Safely tries to convert a string to a nullable decimal.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>
        /// The decimal inside the supplied string, or null wrapped in a nullable decimal if the conversion fails.
        /// </returns>
        public static decimal? ToNullableDecimal( string s )
        {
            return ConversionTool.ToNullableDecimal( s, CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Safely tries to convert a string to a nullable decimal using the specified format provider.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// The decimal inside the supplied string, or null wrapped in a nullable decimal if the conversion fails.
        /// </returns>
        public static decimal? ToNullableDecimal( string s, IFormatProvider provider )
        {
            decimal d = 0;
            return Decimal.TryParse( s, NumberStyles.Any, provider, out d ) ? d : (decimal?)null;
        }

        /// <summary>
        /// Safely tries to convert an object to an integer, and if the conversion fails, returns 0.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>The integer inside the supplied object, or 0 if the conversion fails.</returns>
        public static int ToInteger( object o )
        {
            return ConversionTool.ToInteger( o != null ? o.ToString() : "" );
        }

        /// <summary>
        /// Safely tries to convert a string to an integer, and if the conversion fails, returns 0.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The integer inside the supplied string, or 0 if the conversion fails.</returns>
        public static int ToInteger( string s )
        {
            return ConversionTool.ToInteger( s, 0 );
        }

        /// <summary>
        /// Safely tries to convert a string to an integer, and if the conversion fails, returns a specified value.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="toReturnIfConversionFails">The value that will be returned if the conversion fails.</param>
        /// <returns>
        /// The integer inside the supplied string, or the value of 
        /// <paramref name="toReturnIfConversionFails"/> if the conversion fails.
        /// </returns>
        public static int ToInteger( string s, int toReturnIfConversionFails )
        {
            return ConversionTool.ToInteger( s, toReturnIfConversionFails, CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Safely tries to convert a string to an integer using the specified format provider.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The integer inside the supplied string, or 0 if the conversion fails.</returns>
        public static int ToInteger( string s, IFormatProvider provider )
        {
            return ConversionTool.ToInteger( s, 0, provider );
        }

        /// <summary>
        /// Safely tries to convert a string to an integer using the specified format provider, and 
        /// if the conversion fails, returns a specified value.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="toReturnIfConversionFails">The value that will be returned if the conversion fails.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// The integer inside the supplied string, or the value of 
        /// <paramref name="toReturnIfConversionFails"/> if the conversion fails.
        /// </returns>
        public static int ToInteger( string s, int toReturnIfConversionFails, IFormatProvider provider )
        {
            int i = 0;
            return Int32.TryParse( s, NumberStyles.Any, provider, out i ) ? i : toReturnIfConversionFails;
        }

        /// <summary>
        /// Safely tries to convert an object to a nullable integer.
        /// </summary>
        /// <param name="o">The object to convert.</param>
        /// <returns>
        /// The integer inside the supplied object, or null wrapped in a nullable integer if the conversion fails.
        /// </returns>
        public static int? ToNullableInteger( object o )
        {
            return ConversionTool.ToNullableInteger( o != null ? o.ToString() : "" );
        }

        /// <summary>
        /// Safely tries to convert a string to a nullable integer.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>
        /// The integer inside the supplied string, or null wrapped in a nullable integer if the conversion fails.
        /// </returns>
        public static int? ToNullableInteger( string s )
        {
            return ConversionTool.ToNullableInteger( s, CultureInfo.InvariantCulture );
        }

        /// <summary>
        /// Safely tries to convert a string to a nullable integer using the specified format provider.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>
        /// The integer inside the supplied string, or null wrapped in a nullable integer if the conversion fails.
        /// </returns>
        public static int? ToNullableInteger( string s, IFormatProvider provider )
        {
            int i = 0;
            return Int32.TryParse( s, NumberStyles.Any, provider, out i ) ? i : (int?)null;
        }
    }
}