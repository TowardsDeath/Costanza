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
                string s = "Malcolm Tucker";
              
                // Act
                decimal returned = ConversionTool.ToDecimal( s );
               
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFails()
            {
                // Arrange
                string s = "John Duggan";
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
                string s = "1,5 fucking Fanta's";
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
                string s = "Peter Mannion";
                decimal d = 0.5m;
                var culture = new CultureInfo( "nl-NL" );
                
                // Act
                decimal returned = ConversionTool.ToDecimal( s, d, culture );
                
                // Assert
                Assert.Equal( d, returned );
            }

            [Fact]
            public void ConvertsNullStringValue()
            {
                // Arrange
                string s = null;

                // Arrange
                decimal returned = ConversionTool.ToDecimal( s );

                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ConvertsObjectToInteger()
            {
                // Arrange
                decimal i = 50m;

                // Act
                decimal returned = ConversionTool.ToDecimal( (object)i );

                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ConvertsNullObjectValue()
            {
                // Arrange
                object o = null;

                // Act
                decimal returned = ConversionTool.ToDecimal( o );

                // Assert
                Assert.Equal( 0, returned );
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
                string s = "Ollie Reeder";

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
                string s = "2,5 cunts";
                var culture = new CultureInfo( "nl-NL" );
                
                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s, culture );
                
                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ConvertsNullStringValue()
            {
                // Arrange
                string s = null;

                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( s );
                
                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ConvertsObjectToInteger()
            {
                // Arrange
                decimal i = 50m;

                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( (object)i );

                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ConvertsNullObjectValue()
            {
                // Arrange
                object o = null;

                // Act
                decimal? returned = ConversionTool.ToNullableDecimal( o );

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
                string s = "Tinker Tailor Soldier Cunt";
                
                // Act
                int returned = ConversionTool.ToInteger( s );
                
                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ReturnsValueWhenConversionFails()
            {
                // Arrange
                string s = "Nicola Murray";
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
                string s = "15,0";
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
                string s = "2 bastards";
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
                string s = "Stewart Pearson";
                int i = 16;
                var culture = new CultureInfo( "nl-NL" );
                
                // Act
                int returned = ConversionTool.ToInteger( s, i, culture );
                
                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ConvertsNullStringValue()
            {
                // Arrange
                string s = null;

                // Act
                int returned = ConversionTool.ToInteger( s );

                // Assert
                Assert.Equal( 0, returned );
            }

            [Fact]
            public void ConvertsObjectToInteger()
            {
                // Arrange
                int i = 50;

                // Act
                int returned = ConversionTool.ToInteger( (object)i );

                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ConvertsNullObjectValue()
            {
                // Arrange
                object o = null;

                // Act
                int returned = ConversionTool.ToInteger( o );

                // Assert
                Assert.Equal( 0, returned );
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
                string s = "Phil Smith";
                // Act
                int? returned = ConversionTool.ToNullableInteger( s );
                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void RespectsCultureWhenConverting()
            {
                // Arrange
                string s = "25,0";
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
                string s = "4 biscuits";
                var culture = new CultureInfo( "nl-NL" );

                // Act
                int? returned = ConversionTool.ToNullableInteger( s, culture );

                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ConvertsNullStringValue()
            {
                // Arrange
                string s = null;

                // Act
                int? returned = ConversionTool.ToNullableInteger( s );

                // Assert
                Assert.Equal( null, returned );
            }

            [Fact]
            public void ConvertsObjectToInteger()
            {
                // Arrange
                int i = 50;

                // Act
                int? returned = ConversionTool.ToNullableInteger( (object)i );

                // Assert
                Assert.Equal( i, returned );
            }

            [Fact]
            public void ConvertsNullObjectValue()
            {
                // Arrange
                object o = null;

                // Act
                int? returned = ConversionTool.ToNullableInteger( o );

                // Assert
                Assert.Equal( null, returned );
            }
        }
    }
}