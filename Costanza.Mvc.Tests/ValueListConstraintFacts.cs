using System.Web.Mvc;
using System.Web.Routing;

using Costanza.Mvc.TestHelpers;

using Xunit;

namespace Costanza.Mvc.Tests
{
    public class ValueListConstraintFacts
    {
        protected readonly RouteCollection Routes;

        public ValueListConstraintFacts()
        {
            this.Routes = new RouteCollection();
            this.Routes.MapRoute( 
                "ValueRoute",
                "home/{value}",
                new { controller = "home", action = "index" },
                new { value = new ValueListConstraint( "one", "two" ) } );
        }

        [Fact]
        public void RouteWithValueIsMatched()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/one" );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.NotNull( routeData );
            Assert.Equal( "home", routeData.Values[ "controller" ] );
            Assert.Equal( "index", routeData.Values[ "action" ] );
            Assert.Equal( "one", routeData.Values[ "value" ] );
        }

        [Fact]
        public void RouteWithoutValueIsSkipped()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/three" );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.Null( routeData );
        }
    }
}