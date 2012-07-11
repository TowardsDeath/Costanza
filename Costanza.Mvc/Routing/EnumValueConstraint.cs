using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public class EnumValueConstraint<TEnum, TEnumType> : IRouteConstraint where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        protected readonly HashSet<TEnumType> EnumValues;

        public EnumValueConstraint()
        {
            var values = Enum.GetValues( typeof( TEnum ) ).Cast<TEnumType>();
            this.EnumValues = new HashSet<TEnumType>( from v in values
                                                   select v );
        }

        public bool Match( HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection )
        {
            var value = (TEnumType)Convert.ChangeType( values[ parameterName ], typeof( TEnumType ) );
            return this.EnumValues.Contains( value );
        }
    }
}