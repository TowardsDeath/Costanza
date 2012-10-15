using System.Web.Mvc;
using System.Web.Routing;

using Costanza.Mvc.TestHelpers;

using Xunit;

namespace Costanza.Mvc.Tests
{
    public class EnumValueConstraintFacts
    {
        protected readonly RouteCollection Routes;

        public EnumValueConstraintFacts()
        {
            this.Routes = new RouteCollection();
            this.Routes.MapRoute( 
                "EnumRoute",
                "home/{value}",
                new { controller = "home", action = "index" },
                new { value = new EnumValueConstraint<TestEnum>() } );
        }

        [Fact]
        public void RouteWithEnumValueIsMatched()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/{0}".FormatWith( (int)TestEnum.Two ) );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.NotNull( routeData );
            Assert.Equal( "home", routeData.Values[ "controller" ] );
            Assert.Equal( "index", routeData.Values[ "action" ] );
            Assert.Equal( (int)TestEnum.Two, ConversionTool.ToInteger( routeData.Values[ "value" ] ) );
        }

        [Fact]
        public void RouteWithoutEnumValueIsSkipped()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/3" );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.Null( routeData );
        }

        public enum TestEnum : int
        {
            One = 1,
            Two = 2
        }
    }
}