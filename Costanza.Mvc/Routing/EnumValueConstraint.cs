using System;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Restricts route parameters to the values of the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum whose values are used as a restriction.</typeparam>
    public class EnumValueConstraint<TEnum> : IRouteConstraint where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        /// <summary>
        /// Determines whether the value of the URL parameter is present in the enum as a value.
        /// </summary>
        /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
        /// <param name="route">The object that this constraint belongs to.</param>
        /// <param name="parameterName">The name of the parameter that is being checked.</param>
        /// <param name="values">An object that contains the parameters for the URL.</param>
        /// <param name="routeDirection">
        /// An object that indicates whether the constraint check is being performed
        /// when an incoming request is being handled or when a URL is being generated.
        /// </param>
        /// <returns>True if the value of the URL parameter is present in the enum. Otherwise, false.</returns>
        public bool Match( 
            HttpContextBase httpContext, 
            Route route, 
            string parameterName, 
            RouteValueDictionary values, 
            RouteDirection routeDirection )
        {
            if( !values.ContainsKey( parameterName ) ) return false;

            var underlyingType = Enum.GetUnderlyingType( typeof( TEnum ) );
            var value = (TEnum)Convert.ChangeType( values[ parameterName ], underlyingType );

            return Enum.IsDefined( typeof( TEnum ), value );
        }
    }
}