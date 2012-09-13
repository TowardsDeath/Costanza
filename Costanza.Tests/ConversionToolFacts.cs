using System.Globalization;

using Xunit;

namespace Costanza.Tests
{
    public class ConversionToolFacts
    {
        public class ConvertToDecimalMethod
        {
            [Fact]
            public void ConvertsDecimalInString()
            {
                // Arrange
                decimal d = 0.75m;
                string s = d.ToString( CultureInfo.InvariantCulture );
                // Act
                decimal returned = ConversionTool.ToDecimal( s );
                // Assert
                Assert.Equal( d, returned );
            }

            [Fact]
            public void ReturnsZeroWhenConversionFails()
            {
                // Arrange
                string s = "Newman";
                // Act
                decimal returned = ConversionTool.ToDecimal( s );
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFails()
            {
                // Arrange
                string s = "Cosmo";
                decimal d = 0.5m;
                // Act
                decimal returned = ConversionTool.ToDecimal( s, d );
                // Assert
                Assert.Equal( d, returned );
            }

            [Fact]
            public void RespectsCultureWhenConverting()
            {
                // Arrange
                string s = "2,5";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                decimal returned = ConversionTool.ToDecimal( s, culture );
                // Assert
                Assert.Equal( 2.5m, returned );
            }

            [Fact]
            public void ReturnsZeroWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "1,5 Leederacola";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                decimal returned = ConversionTool.ToDecimal( s, culture );
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "Bob Sacamano";
                decimal d = 0.5m;
                var culture = new CultureInfo( "nl-NL" );
                // Act
                decimal returned = ConversionTool.ToDecimal( s, d, culture );
                // Assert
                Assert.Equal( d, returned );
            }
        }

        public class ConvertToNullableDecimalMethod
        {
            [Fact]
            public void ConvertsDecimalInString()
            {
                // Arrange
                decimal d = 3.45m;
                string s = d.ToString( CultureInfo.InvariantCulture );
                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s );
                // Assert
                Assert.Equal( d, returned );
            }

            [Fact]
            public void ReturnsNullWhenConversionFails()
            {
                // Arrange
                string s = "Cousin Jeffrey";
                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s );
                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void RespectsCultureWhenConverting()
            {
                // Arrange
                string s = "2,6";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s, culture );
                // Assert
                Assert.Equal( 2.6m, returned );
            }

            [Fact]
            public void ReturnsNullWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "2,5 meow";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s, culture );
                // Assert
                Assert.Equal( null, returned );
            }
        }

        public class ConvertToIntegerMethod
        {
            [Fact]
            public void ConvertsIntegerInString()
            {
                // Arrange
                int i = 11;
                string s = i.ToString( CultureInfo.InvariantCulture );
                // Act
                int returned = ConversionTool.ToInteger( s );
                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ReturnsZeroWhenConversionFails()
            {
                // Arrange
                string s = "Ping";
                // Act
                int returned = ConversionTool.ToInteger( s );
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFails()
            {
                // Arrange
                string s = "Mr Pitt";
                int i = 50;
                // Act
                int returned = ConversionTool.ToInteger( s, 50 );
                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void RespectsCultureWhenConverting()
            {
                // Arrange
                string s = "15";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                int returned = ConversionTool.ToInteger( s, culture );
                // Assert
                Assert.Equal( 15, returned );
            }

            [Fact]
            public void ReturnsZeroWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "2 Leederacola";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                int returned = ConversionTool.ToInteger( s, culture );
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "Uncle Leo";
                int i = 16;
                var culture = new CultureInfo( "nl-NL" );
                // Act
                int returned = ConversionTool.ToInteger( s, i, culture );
                // Assert
                Assert.Equal( i, returned );
            }
        }

        public class ConvertToNullableIntegerMethod
        {
            [Fact]
            public void ConvertsIntegerInString()
            {
                // Arrange
                int i = 3;
                string s = i.ToString( CultureInfo.InvariantCulture );
                // Act
                decimal? returned = ConversionTool.ToNullableInteger( s );
                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ReturnsNullWhenConversionFails()
            {
                // Arrange
                string s = "Mickey";
                // Act
                int? returned = ConversionTool.ToNullableInteger( s );
                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void RespectsCultureWhenConverting()
            {
                // Arrange
                string s = "25";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                int? returned = ConversionTool.ToNullableInteger( s, culture );
                // Assert
                Assert.Equal( 25, returned );
            }

            [Fact]
            public void ReturnsNullWhenConversionFailsWithCulture()
            {
                // Arrange
                string s = "4 Leederacola";
                var culture = new CultureInfo( "nl-NL" );
                // Act
                int? returned = ConversionTool.ToNullableInteger( s, culture );
                // Assert
                Assert.Equal( null, returned );
            }
        }
    }
}