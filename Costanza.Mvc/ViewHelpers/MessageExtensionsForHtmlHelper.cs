using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    public static class MessageExtensionsForHtmlHelper
    {
        public static MvcHtmlString PostNotice( this HtmlHelper helper, string notice, object htmlAttributes )
        {
            if( helper == null ) throw new ArgumentNullException( "helper" );
            if( notice.IsBlank() ) throw new ArgumentNullException( "notice" );

            var div = new TagBuilder( "div" );
            div.MergeAttributes( HtmlHelper.AnonymousObjectToHtmlAttributes( htmlAttributes ), false );
            div.InnerHtml += notice;
            return MvcHtmlString.Create( div.ToString( TagRenderMode.Normal ) );
        }
    }
}