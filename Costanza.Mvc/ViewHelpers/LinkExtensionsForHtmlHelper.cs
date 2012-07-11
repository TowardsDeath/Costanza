using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public static class LinkExtensionsForHtmlHelper
    {
        public static MvcHtmlString UrlLink( this HtmlHelper helper, string url, string text )
        {
            return helper.UrlLink( url, text, null );

        }
        public static MvcHtmlString UrlLink( this HtmlHelper helper, string url, string text, object htmlAttributes )
        {
            return helper.UrlLink( url, text, new RouteValueDictionary( htmlAttributes ) );
        }

        public static MvcHtmlString UrlLink( this HtmlHelper helper, string url, string text, RouteValueDictionary htmlAttributes )
        {
            var a = new TagBuilder( "a" );
            a.Attributes.Add( "href", url );
            a.MergeAttributes( htmlAttributes, false );
            a.SetInnerText( text );

            return MvcHtmlString.Create( a.ToString( TagRenderMode.Normal ) );
        }
    }
}