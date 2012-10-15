using System;

namespace Costanza
{
    /// <summary>
    /// Provides utility methods for date calculations.
    /// </summary>
    public static class DateTimeTool
    {
        /// <summary>
        /// Determines if the supplied date is at least a year ago compared to the current date.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>A boolean indicating whether the supplied date is at least a year ago.</returns>
        public static bool IsAtLeastAYearAgo( DateTimeOffset date )
        {
            return Math.Abs( ( DateTimeOffset.UtcNow - date ).Days ) > 365;
        }

        /// <summary>
        /// Determines if the suplied date is at least a year away from now.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>A boolean indicating whether the supplied date is at least a year away from now.</returns>
        public static bool IsAtLeastAYearFromNow( DateTimeOffset date )
        {
            return IsAtLeastAYearAgo( date );
        }

        /// <summary>
        /// Determines if the current point of time lies in the range defined by
        /// the supplied start and end date.
        /// </summary>
        /// <param name="start">The start of the date range.</param>
        /// <param name="end">The end of the date range.</param>
        /// <returns>
        /// A boolean indicating whether the current point of time falls in the supplied date range.
        /// </returns>
        public static bool IsNowInRange( DateTimeOffset start, DateTimeOffset end )
        {
            var now = DateTimeOffset.UtcNow;
            return start < now && end > now;
        }

        /// <summary>
        /// Determines if the supplied date is today.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns>A boolean indicating whether the supplied date is today.</returns>
        public static bool IsToday( DateTimeOffset date )
        {
            return date.Date.Equals( DateTimeOffset.UtcNow.Date );
        }

        /// <summary>
        /// Determines if the supplied date range spans multiple days.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        /// <returns>A boolean indicating whether the supplied date range spans multiple days.</returns>
        public static bool SpansMultipleDays( DateTimeOffset start, DateTimeOffset end )
        {
            if( ( end - start ).Hours < 24 )
            {
                return start.Day != end.Day;
            }
            return ( end - start ).Days >= 1;
        }

        /// <summary>
        /// Determines if the supplied date range spans multiple months.
        /// </summary>
        //// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        /// <returns>A boolean indicating whether the supplied date range spans multiple months.</returns>
        public static bool SpansMultipleMonths( DateTimeOffset start, DateTimeOffset end )
        {
            bool sameMonth = start.Month == end.Month;
            bool sameYear = start.Year == end.Year;
            return sameYear ? !sameMonth : !sameMonth && !sameYear;
        }
    }
}