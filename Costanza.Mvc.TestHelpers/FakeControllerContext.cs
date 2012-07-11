using System.Web.Mvc;
using System.Web.Routing;

using Moq;

namespace Costanza.Mvc.TestHelpers
{
    public static class FakeControllerContext
    {
        public static ControllerContext New()
        {
            var controllerContext = new Mock<ControllerContext>();
            var httpContext = FakeHttpContext.New();
            var routes = new RouteData();
            var controllerBase = new Mock<ControllerBase>();
            
            controllerContext.Setup( c => c.HttpContext ).Returns( httpContext );
            controllerContext.Setup( c => c.Controller ).Returns( controllerBase.Object );
            controllerContext.Setup( c => c.RouteData ).Returns( routes );

            return controllerContext.Object;
        }
    }
}