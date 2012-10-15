using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace Costanza.Mvc
{
    /// <summary>
    /// An action result that renders an Atom feed to the response.
    /// </summary>
    public class AtomResult : ActionResult
    {
        /// <summary>
        /// The feed with items.
        /// </summary>
        protected readonly SyndicationFeed Feed;

        /// <summary>
        /// Iniitializes a new instance with the supplied syndication feed.
        /// </summary>
        /// <param name="feed">The feed to turn into Atom.</param>
        public AtomResult( SyndicationFeed feed )
        {
            this.Feed = feed;
        }

        /// <summary>
        /// Turns the feed items into Atom XML that can be used for syndication.
        /// </summary>
        /// <param name="context">The context of the controller in which the result is executed.</param>
        public override void ExecuteResult( ControllerContext context )
        {
            context.HttpContext.Response.ContentType = "application/atom+xml";
            using( var writer = XmlWriter.Create( context.HttpContext.Response.Output ) )
            {
                var formatter = new Atom10FeedFormatter( this.Feed );
                formatter.WriteTo( writer );
            }
        }
    }
}