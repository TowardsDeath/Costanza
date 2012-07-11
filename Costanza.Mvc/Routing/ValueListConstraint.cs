using System.Collections.Generic;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public class ValueListConstraint : IRouteConstraint
    {
        protected readonly HashSet<string> AllowedParameters;

        public ValueListConstraint( params string[] allowedParameters )
        {
            this.AllowedParameters = new HashSet<string>( allowedParameters );
        }

        public bool Match( HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection )
        {
            if( values.ContainsKey( parameterName ) ) return false;

            string value = values[ parameterName ].ToString();
            return this.AllowedParameters.Contains( value );
        }
    }
}