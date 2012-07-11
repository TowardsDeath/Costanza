using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public static class QueryStringExtensionsForUrlHelper
    {
        public static string RemoveParameters( this UrlHelper helper, params string[] parameters )
        {
            var queryString = HttpUtility.ParseQueryString( helper.RequestContext.HttpContext.Request.Url.Query );
            foreach( var p in parameters )
            {
                queryString.Remove( p );
            }

            return string.Concat( helper.Action( "" ), queryString.Count == 0 ? "" : "?", queryString );
        }

        public static string SetParameters( this UrlHelper helper, object parameters )
        {
            return helper.SetParameters( new RouteValueDictionary( parameters ) );
        }

        public static string SetParameters( this UrlHelper helper, RouteValueDictionary parameters )
        {
            var queryString = HttpUtility.ParseQueryString( helper.RequestContext.HttpContext.Request.Url.Query );
            foreach( var p in parameters )
            {
                if( ( p.Value as string ).IsBlank() )
                {
                    queryString.Remove( p.Key );
                    continue;
                }

                queryString[ p.Key.ToLowerInvariant() ] = p.Value == null ? null : p.Value.ToString();
            }

            return string.Concat( helper.Action( "" ), queryString.Count == 0 ? "" : "?", queryString );
        }
    }
}