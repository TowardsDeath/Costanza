using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public class EnumNameConstraint<TEnum> : IRouteConstraint where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        protected readonly HashSet<string> EnumNames;

        public EnumNameConstraint()
        {
            var names = Enum.GetNames( typeof( TEnum ) );
            this.EnumNames = new HashSet<string>( from name in names
                                                  select name.ToLowerInvariant() );
        }

        public bool Match( HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection )
        {
            string value = values[ parameterName ] as string;
            if( value.IsBlank() ) return false;

            return this.EnumNames.Contains( value.ToLowerInvariant() );
        }
    }
}