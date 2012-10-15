using System;

using Xunit;

namespace Costanza.Tests
{
    public class DateTimeToolFacts
    {
        public class IsAtLeastAYearAgoMethod
        {
            [Fact]
            public void CorrectlyDeterminesDateFromMoreThanAYearAgo()
            {
                // Arrange
                var date = DateTimeOffset.UtcNow.AddYears( -1 ).AddDays( -1 );

                // Act
                bool returned = DateTimeTool.IsAtLeastAYearAgo( date );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyDeterminesNow()
            {
                // Arrange
                var now = DateTimeOffset.UtcNow;

                // Act
                bool returned = DateTimeTool.IsAtLeastAYearAgo( now );

                // Assert
                Assert.Equal( false, returned );
            }
        }

        public class IsAtLeastAYearFromNowMethod
        {
            [Fact]
            public void CorrectlyDeterminesDateFromMoreThanAYearFromNow()
            {
                // Arrange
                var date = DateTimeOffset.UtcNow.AddYears( 1 ).AddDays( 1 );

                // Act
                bool returned = DateTimeTool.IsAtLeastAYearFromNow( date );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyDeterminesNow()
            {
                // Arrange
                var now = DateTimeOffset.UtcNow;

                // Act
                bool returned = DateTimeTool.IsAtLeastAYearFromNow( now );

                // Assert
                Assert.Equal( false, returned );
            }
        }

        public class IsNowInRangeMethod
        {
            [Fact]
            public void CorrectlyDeterminesWithinRange()
            {
                // Arrange
                var start = DateTimeOffset.UtcNow.AddDays( -1 );
                var end = DateTimeOffset.UtcNow.AddDays( 1 );

                // Act
                bool returned = DateTimeTool.IsNowInRange( start, end );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyDeterminesOutsideRange()
            {
                // Arrange
                var start = DateTimeOffset.UtcNow.AddDays( -2 );
                var end = DateTimeOffset.UtcNow.AddDays( -1 );

                // Act
                bool returned = DateTimeTool.IsNowInRange( start, end );

                // Assert
                Assert.Equal( false, returned );
            }
        }

        public class IsTodayMethod
        {
            [Fact]
            public void ReturnsTrueForToday()
            {
                // Arrange
                var now = DateTimeOffset.UtcNow;

                // Act
                bool returned = DateTimeTool.IsToday( now );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void ReturnsFalseForTomorrow()
            {
                // Arrange
                var tomorrow = DateTimeOffset.UtcNow.AddDays( 1 );

                // Act
                bool returned = DateTimeTool.IsToday( tomorrow );

                // Assert
                Assert.Equal( false, returned );
            }
        }

        public class SpansMultipleDaysMethod
        {
            [Fact]
            public void CorrectlyHandlesAFewHoursApartOnDifferentDays()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 12, 31, 23, 0, 0 ) ); // 31 December 2012 23:00
                var end = new DateTimeOffset( new DateTime( 2013, 1, 1, 1, 0, 0 ) ); // 1 January 2013 1:00

                // Act
                bool returned = DateTimeTool.SpansMultipleDays( start, end );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyHandlesAFewHoursApartOnSameDay()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 8, 8, 17, 0, 0 ) ); // 8 August 17:00
                var end = new DateTimeOffset( new DateTime( 2012, 8, 8, 23, 0, 0 ) ); // 8 August 23:00

                // Act
                bool returned = DateTimeTool.SpansMultipleDays( start, end );

                // Assert
                Assert.Equal( false, returned );
            }

            [Fact]
            public void CorrectlyHandlesExactlyADayApart()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 8, 8, 17, 0, 0 ) ); // 8 August 17:00
                var end = new DateTimeOffset( new DateTime( 2012, 8, 9, 17, 0, 0 ) ); // 9 August 17:00

                // Act
                bool returned = DateTimeTool.SpansMultipleDays( start, end );

                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyHandlesMoreThanADayApart()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 8, 8, 17, 0, 0 ) ); // 8 August 17:00
                var end = new DateTimeOffset( new DateTime( 2012, 8, 9, 18, 0, 0 ) ); // 9 August 18:00

                // Act
                bool returned = DateTimeTool.SpansMultipleDays( start, end );

                // Assert
                Assert.Equal( true, returned );
            }
        }

        public class SpansMultipleMonthsMethod
        {
            [Fact]
            public void CorrectlyHandlesMoreThanAMonthApart()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 1, 1 ) ); // 1 January 2012
                var end = new DateTimeOffset( new DateTime( 2012, 3, 1 ) ); // 1 March 2012
                
                // Act
                bool returned = DateTimeTool.SpansMultipleMonths( start, end );
                
                // Assert
                Assert.Equal( true, returned );
            }

            [Fact]
            public void CorrectlyHandlesLessThanAMonthApart()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 1, 1 ) ); // 1 January 2012
                var end = new DateTimeOffset( new DateTime( 2012, 1, 15 ) ); // 15 January 2012
                
                // Act
                bool returned = DateTimeTool.SpansMultipleMonths( start, end );
                
                // Assert
                Assert.Equal( false, returned );
            }

            [Fact]
            public void CorrectlyHandlesLessThanAMonthApartInDifferentYears()
            {
                // Arrange
                var start = new DateTimeOffset( new DateTime( 2012, 12, 31 ) ); // 31 December 2012
                var end = new DateTimeOffset( new DateTime( 2013, 1, 1 ) ); // 1 January 2013
               
                // Act
                bool returned = DateTimeTool.SpansMultipleMonths( start, end );
               
                // Assert
                Assert.Equal( true, returned );
            }
        }
    }
}