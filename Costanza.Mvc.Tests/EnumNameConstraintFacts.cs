using System.Web.Mvc;
using System.Web.Routing;

using Costanza.Mvc.TestHelpers;

using Xunit;

namespace Costanza.Mvc.Tests
{
    public class EnumNameConstraintFacts
    {
        protected readonly RouteCollection Routes;

        public EnumNameConstraintFacts()
        {
            this.Routes = new RouteCollection();
            this.Routes.MapRoute( 
                "EnumRoute",
                "home/{value}",
                new { controller = "home", action = "index" },
                new { value = new EnumNameConstraint<TestEnum>() } );
        }

        [Fact]
        public void RouteWithEnumNameIsMatched()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/{0}".FormatWith( TestEnum.One ) );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.NotNull( routeData );
            Assert.Equal( "home", routeData.Values[ "controller" ] );
            Assert.Equal( "index", routeData.Values[ "action" ] );
            Assert.Equal( TestEnum.One.ToString(), routeData.Values[ "value" ] );
        }

        [Fact]
        public void RouteWithoutEnumNameIsSkipped()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/home/1" );

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