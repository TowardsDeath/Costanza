using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Restricts route parameters to the specified list of values.
    /// </summary>
    public class ValueListConstraint : IRouteConstraint
    {
        /// <summary>
        /// The list of allowed values of route parameter.
        /// </summary>
        protected readonly HashSet<string> AllowedValues;

        /// <summary>
        /// Initializes a new instance of the <c>ValueListConstraint</c> class
        /// with the specified allowed values.
        /// </summary>
        /// <param name="allowedValues"></param>
        public ValueListConstraint( params string[] allowedValues )
        {
            this.AllowedValues = new HashSet<string>( allowedValues );
        }

        /// <summary>
        /// Determines whether the value of the URL parameter is an allowed value.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">
        /// An object that indicates whether the constraint check is being performed
        /// when an incoming request is being handled or when a URL is being generated.
        /// </param>
        /// <returns>True if the route parameter value is present in the list of allowed values.</returns>
        public bool Match( 
            HttpContextBase httpContext, 
            Route route, 
            string parameterName, 
            RouteValueDictionary values, 
            RouteDirection routeDirection )
        {
            if( !values.ContainsKey( parameterName ) ) return false;

            string value = values[ parameterName ].ToString();
            return this.AllowedValues.Contains( value );
        }
    }
}