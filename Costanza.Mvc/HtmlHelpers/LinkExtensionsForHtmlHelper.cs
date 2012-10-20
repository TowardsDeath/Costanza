using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Extensions to HtmlHelper for generating HTML hyperlinks.
    /// </summary>
    public static class LinkExtensionsForHtmlHelper
    {
        /// <summary>
        /// Creates HTML for a hyperlink to the specified URL, with the specified display text.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="url">The URL to link to.</param>
        /// <param name="text">The text to display.</param>
        /// <returns>The generated HTML containing the hyperlink.</returns>
        public static MvcHtmlString UrlLink( this HtmlHelper helper, string url, string text )
        {
            return helper.UrlLink( url, text, null );
        }

        /// <summary>
        /// Creates HTML for a hyperlink to the specified URL, with the specified display text and specified attributes.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="url">The URL to link to.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="htmlAttributes">The attributes to add to the hyperlink.</param>
        /// <returns>The generated HTML containing the hyperlink.</returns>
        public static MvcHtmlString UrlLink( this HtmlHelper helper, string url, string text, object htmlAttributes )
        {
            var normalizedAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes( htmlAttributes );
            return UrlLink( helper, url, text, normalizedAttributes );
        }

        /// <summary>
        /// Creates HTML for a hyperlink to the specified URL, with the specified display text and specified attributes.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="url">The URL to link to.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="htmlAttributes">The attributes to add to the hyperlink.</param>
        /// <returns>The generated HTML containing the hyperlink.</returns>
        private static MvcHtmlString UrlLink( HtmlHelper helper, string url, string text, RouteValueDictionary htmlAttributes )
        {
            var a = new TagBuilder( "a" );
            a.Attributes.Add( "href", url );
            a.MergeAttributes( htmlAttributes, replaceExisting: false );
            a.SetInnerText( text );

            return MvcHtmlString.Create( a.ToString( TagRenderMode.Normal ) );
        }
    }
}