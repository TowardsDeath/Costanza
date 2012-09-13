using System;

namespace Costanza
{
    public static class DateTimeTool
    {
        public static bool IsAtLeastAYearAgo( DateTimeOffset date )
        {
            return Math.Abs( ( DateTimeOffset.UtcNow - date ).Days ) > 365;
        }

        public static bool IsAtLeastAYearFromNow( DateTimeOffset date )
        {
            return IsAtLeastAYearAgo( date );
        }

        public static bool IsOngoing( DateTimeOffset start, DateTimeOffset end )
        {
            var now = DateTimeOffset.UtcNow;
            return start < now && end > now;
        }

        public static bool IsToday( DateTimeOffset date )
        {
            return date.Date.Equals( DateTimeOffset.UtcNow.Date );
        }

        public static bool SpansMultipleDays( DateTimeOffset start, DateTimeOffset end )
        {
            if( ( end - start ).Hours < 24 )
            {
                return start.Day != end.Day;
            }
            return ( end - start ).Days >= 1;
        }

        public static bool SpansMultipleMonths( DateTimeOffset start, DateTimeOffset end )
        {
            bool sameMonth = start.Month == end.Month;
            bool sameYear = start.Year == end.Year;
            return sameYear ? !sameMonth : !sameMonth && !sameYear;
        }
    }
}