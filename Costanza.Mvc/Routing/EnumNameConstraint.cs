using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Restricts route parameters to the names of the specified enum type.
    /// </summary>
    /// <typeparam name="TEnum">The enum whose names are used as a restriction.</typeparam>
    public class EnumNameConstraint<TEnum> : IRouteConstraint where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        /// <summary>
        /// Fast lookup of allowed enum names.
        /// </summary>
        protected readonly HashSet<string> EnumNames;

        /// <summary>
        /// Initializes a new instance of the <c>EnumNameConstraint</c> class.
        /// </summary>
        public EnumNameConstraint()
        {
            var names = Enum.GetNames( typeof( TEnum ) );
            this.EnumNames = new HashSet<string>( from name in names
                                                  select name.ToLowerInvariant() );
        }

        /// <summary>
        /// Determines whether the value of the URL parameter is present in the enum as a name.
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
            string value = values[ parameterName ] as string;
            if( value.IsBlank() ) return false;

            return this.EnumNames.Contains( value.ToLowerInvariant() );
        }
    }
}