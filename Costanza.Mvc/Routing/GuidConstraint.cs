using System;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Restricts route parameters to a Guid format.
    /// </summary>
    public class GuidConstraint : IRouteConstraint
    {
        /// <summary>
        /// Determines whether the value of the URL parameter is a valid Guid.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">
        /// An object that indicates whether the constraint check is being performed
        /// when an incoming request is being handled or when a URL is being generated.
        /// </param>
        /// <returns>True if the route parameter value is a valid Guid. Otherwise, false.</returns>
        public bool Match( 
            HttpContextBase httpContext, 
            Route route, 
            string parameterName, 
            RouteValueDictionary values, 
            RouteDirection routeDirection )
        {
            if( !values.ContainsKey( parameterName ) ) return false;

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