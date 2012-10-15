using System;
using System.Globalization;

using Xunit;

namespace Costanza.Tests
{
    public class ExtensionsForStringFacts
    {
        public class EmptyToNullMethod
        {
            [Fact]
            public void ReturnsNullWhenStringIsEmpty()
            {
                // Arrange
                string s = "";

                // Act
                string returned = s.EmptyToNull();

                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ReturnsNullWhenStringIsNull()
            {
                // Arrange
                string s = null;

                // Act
                var returned = s.EmptyToNull();

                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ReturnsSameValueWhenStringIsNotEmpty()
            {
                // Arrange
                string s = "Art Vandelay";

                // Act
                string returned = s.EmptyToNull();

                // Assert
                Assert.Equal( s, returned );
            }
        }

        public class FormatWithMethod
        {
            [Fact]
            public void FormatsAllSuppliedArguments()
            {
                // Arrange
                string s = "#{3}: The {0} Extinction Crept Up {2}, Like Sunlight Through The {1} As We Looked Back In Regret";
                var parameters = new object[] { "Sixth", "Shutters", "Slowly", 7 };

                // Act
                string returned = s.FormatWith( parameters  );
                string expected = "#7: The Sixth Extinction Crept Up Slowly, Like Sunlight Through The Shutters As We Looked Back In Regret";
                
                // Assert
                Assert.Equal( expected, returned );
            }

            [Fact]
            public void RespectsCulture()
            {
                // Arrange
                string s = "Fulham average {0} points per away game in May (the fourth best rate of all PL teams), compared to just {1} overall. Scramble.";
                var parameters = new object[] { 1.58m, 0.76m };
                var culture = new CultureInfo( "nl-NL" );
                
                // Act
                string returned = s.FormatWith( culture, parameters );
                string expected = "Fulham average 1,58 points per away game in May (the fourth best rate of all PL teams), compared to just 0,76 overall. Scramble.";
                
                // Assert
                Assert.Equal( expected, returned );
            }
        }

        public class HasValueMethod
        {
            [Fact]
            public void ReturnsFalseIfStringIsEmpty()
            {
                // Arrange
                string s = "";

                // Act
                bool b = s.HasValue();

                // Assert
                Assert.Equal( false, b );
            }

            [Fact]
            public void ReturnsFalseIfStringIsNull()
            {
                // Arrange
                string s = null;

                // Act
                bool b = s.HasValue();

                // Assert
                Assert.Equal( false, b );
            }

            [Fact]
            public void ReturnsFalseIfStringIsWhiteSpace()
            {
                // Arrange
                string s = "\r\n ";

                // Act
                bool b = s.HasValue();

                // Assert
                Assert.Equal( false, b );
            }

            [Fact]
            public void ReturnsTrueIfStringConsistsOfCharacters()
            {
                // Arrange
                string s = "Vandelay Industries";

                // Act
                bool b = s.HasValue();

                // Assert
                Assert.Equal( true, b );
            }
        }

        public class IsBlankMethod
        {
            [Fact]
            public void ReturnsTrueIfStringIsEmpty()
            {
                // Arrange
                string s = "";

                // Act
                bool b = s.IsBlank();

                // Assert
                Assert.Equal( true, b );
            }

            [Fact]
            public void ReturnsTrueIfStringIsNull()
            {
                // Arrange
                string s = null;

                // Act
                bool b = s.IsBlank();

                // Assert
                Assert.Equal( true, b );
            }

            [Fact]
            public void ReturnsTrueIfStringIsWhiteSpace()
            {
                // Arrange
                string s = "\r\n ";

                // Act
                bool b = s.IsBlank();

                // Assert
                Assert.Equal( true, b );
            }

            [Fact]
            public void ReturnsFalseIfStringConsistsOfCharacters()
            {
                // Arrange
                string s = "Vandelay Industries";

                // Act
                bool b = s.IsBlank();

                // Assert
                Assert.Equal( false, b );
            }
        }
    }
}