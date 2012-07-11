using System;
using System.Globalization;

namespace Costanza
{
    /// <summary>
    /// General extension methods for instances of <see cref="System.String"/>.
    /// </summary>
    public static class ExtensionsForString
    {
        /// <summary>
        /// Returns null when the supplied string is empty.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>Null if the string is empty. Otherwise, the unaltered value of <paramref name="s"/>.</returns>
        /// <remarks>
        /// Some frameworks process empty strings in a different way than null values. If you ever find yourself
        /// in such a situation, where you need to convert empty strings to null values, this method provides
        /// an easy way to handle that.
        /// </remarks>
        public static string EmptyToNull( this string s )
        {
            return s.IsBlank() ? null : s;
        }

        /// <summary>
        /// Shortcut method for <see cref="System.String.Format"/>.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the string 
        /// representation of the corresponding objects in args.
        /// </returns>
        /// <remarks>
        /// Created because typing <c>"{0} bottles of Bosco".Format( 5 );</c> is more intuitive 
        /// than <c>String.Format( "{0} bottles of Bosco", 5 );</c>.
        /// </remarks>
        public static string FormatWith( this string format, params object[] args )
        {
            return format.FormatWith( CultureInfo.InvariantCulture, args );
        }

        /// <summary>
        /// Shortcut method for <see cref="System.String.Format"/>.
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the string 
        /// representation of the corresponding objects in args.
        /// </returns>
        /// <remarks>
        /// Created because typing <c>"{0} bottles of Bosco".Format( 5 );</c> is more intuitive 
        /// than <c>String.Format( "{0} bottles of Bosco", 5 );</c>.
        /// </remarks>
        public static string FormatWith( this string format, IFormatProvider provider, params object[] args )
        {
            return String.Format( provider, format, args );
        }

        /// <summary>
        /// Indicates whether a string has a usable value.
        /// </summary>
        /// <param name="s">The string to check for contents.</param>
        /// <returns>Returns true if <paramref name="s"/> is <i>not</i> null, empty, or white-space. Otherwise, false.</returns>
        public static bool HasValue( this string s )
        {
            return !s.IsBlank();
        }

        /// <summary>
        /// Indicates whether a string is null, empty, or consisting of only whitespace.
        /// </summary>
        /// <param name="s">The string to check for contents.</param>
        /// <returns>Returns true if <paramref name="s"/> is null, empty, or white-space. Otherwise, false.</returns>
        public static bool IsBlank( this string s )
        {
            return String.IsNullOrWhiteSpace( s );
        }
    }
}