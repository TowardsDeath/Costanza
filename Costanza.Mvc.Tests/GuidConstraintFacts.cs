using System;
using System.Web.Mvc;
using System.Web.Routing;

using Costanza.Mvc;
using Costanza.Mvc.TestHelpers;

using Xunit;

namespace Costanza.Mvc.Tests
{
    public class GuidConstraintFacts
    {
        protected readonly Guid Guid;
        protected readonly RouteCollection Routes;

        public GuidConstraintFacts()
        {
            this.Guid = Guid.NewGuid();
            this.Routes = new RouteCollection();
            this.Routes.MapRoute( "GuidRoute",
                "guids/{id}",
                new { controller = "guid", action = "index" },
                new { id = new GuidConstraint() } );
        }

        [Fact]
        public void RouteWithGuidIsMatched()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/guids/{0}".FormatWith( this.Guid ) );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.NotNull( routeData );
            Assert.Equal( "guid", routeData.Values[ "controller" ] );
            Assert.Equal( "index", routeData.Values[ "action" ] );
            Assert.Equal( this.Guid.ToString(), routeData.Values[ "id" ] );
        }

        [Fact]
        public void RouteWithoutGuidIsSkipped()
        {
            // Arrange
            var httpContext = FakeHttpContext.New( "~/users/5" );

            // Act
            var routeData = this.Routes.GetRouteData( httpContext );

            // Assert
            Assert.Null( routeData );
        }
    }
}