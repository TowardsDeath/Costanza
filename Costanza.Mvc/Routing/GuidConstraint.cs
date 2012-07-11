using System;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public class GuidConstraint : IRouteConstraint
    {
        public bool Match( HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection )
        {
            if( !values.ContainsKey( parameterName ) ) return false;
            if( values[ parameterName ] is Guid ) return true;

            string stringValue = values[ parameterName ] as string;
            if( stringValue.IsBlank() ) return false;

            try
            {
                var guid = new Guid( stringValue );
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}