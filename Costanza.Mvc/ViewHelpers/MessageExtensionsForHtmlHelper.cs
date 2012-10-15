using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Costanza.Mvc
{
    /// <summary>
    /// Extensions to HtmlHelper for generating messages.
    /// </summary>
    public static class MessageExtensionsForHtmlHelper
    {
        /// <summary>
        /// Creates HTML for one-time confirmation message to show the user, if the key is present in TempData.
        /// The message disappears after a refresh.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="key">The key to look for in TempData.</param>
        /// <param name="notice">The text to show to the user.</param>
        /// <returns>The generated HTML with the notice if the key is found. Otherwise, null.</returns>
        public static MvcHtmlString ConfirmationNotice( this HtmlHelper helper, string key, string notice )
        {
            return helper.ConfirmationNotice( key, notice, htmlAttributes: null ); 
        }

        /// <summary>
        /// Creates HTML with the specified attributes, for one-time confirmation message to show the user, 
        /// if the key is present in TempData. The message disappears after a refresh.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="key">The key to look for in TempData.</param>
        /// <param name="notice">The text to show to the user.</param>
        /// <param name="htmlAttributes">The attributes to add to the notice HTML.</param>
        /// <returns>The generated HTML with the notice if the key is found. Otherwise, null.</returns>
        public static MvcHtmlString ConfirmationNotice( this HtmlHelper helper, string key, string notice, object htmlAttributes )
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes( htmlAttributes );
            return ConfirmationNotice( helper, key, notice, attributes );
        }

        /// <summary>
        /// Creates HTML for one-time confirmation message to show the user, if the key is present in TempData.
        /// The message disappears after a refresh.
        /// </summary>
        /// <param name="helper">The current HtmlHelper.</param>
        /// <param name="key">The key to look for in TempData.</param>
        /// <param name="notice">The text to show to the user.</param>
        /// <param name="htmlAttributes">The attributes to add to the notice HTML.</param>
        /// <returns>The generated HTML with the notice if the key is found. Otherwise, null.</returns>
        private static MvcHtmlString ConfirmationNotice( HtmlHelper helper, string key, string notice, RouteValueDictionary htmlAttributes )
        {
            if( helper == null ) throw new ArgumentNullException( "helper" );
            if( notice.IsBlank() ) throw new ArgumentNullException( "notice" );

            bool showNotification = CheckIfKeyIsPresentInTempData( key, helper.ViewContext.TempData );
            if( !showNotification ) return null;

            var div = new TagBuilder( "div" );
            div.MergeAttributes( htmlAttributes, replaceExisting: false );
            div.InnerHtml += notice;
            return MvcHtmlString.Create( div.ToString( TagRenderMode.Normal ) );
        }

        /// <summary>
        /// Checks if the specified key is present in the MVC TempData dictionary.
        /// </summary>
        /// <param name="key">The key to search for.</param>
        /// <param name="tempData">The dictionary to search in.</param>
        /// <returns>True if the specified value is present. Otherwise, false.</returns>
        private static bool CheckIfKeyIsPresentInTempData( string key, TempDataDictionary tempData )
        {
            object tempDataValue;
            tempData.TryGetValue( key, out tempDataValue );

            if( tempDataValue == null ) return false;
            if( (bool)tempDataValue == false ) return false;

            return true;
        }
    }
}