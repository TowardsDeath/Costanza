using System;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Hosting;

namespace Costanza.Mvc
{
    /// <summary>
    /// Action result that writes a web image to the response.
    /// </summary>
    /// <remarks>
    /// The image can be a file on the local file system, or a bytes stream in memory.
    /// </remarks>
    public class ImageResult : ActionResult
    {
        /// <summary>
        /// The path to the image, in case it's a local file.
        /// </summary>
        protected readonly string FilePath;

        /// <summary>
        /// The mime type of the image.
        /// </summary>
        protected readonly string ContentType;

        /// <summary>
        /// The stream containing the image, in case it's located in memory.
        /// </summary>
        protected readonly Stream FileStream;

        /// <summary>
        /// The date the image has been modified, for determining caching.
        /// </summary>
        protected readonly DateTime LastModified;

        /// <summary>
        /// Default buffer size as defined in BufferedStream type, taken from System.Web.Mvc.FileStreamResult.
        /// </summary>
        private const int BufferSize = 0x1000;

        /// <summary>
        /// Constructs a new instance of the <c>ImageResult</c> class
        /// with a path to the local file, and the specified image type.
        /// </summary>
        /// <param name="filePath">The virtual path to the image.</param>
        /// <param name="contentType">The mime type of the image.</param>
        public ImageResult( string filePath, string contentType )
        {
            if( filePath == null )
                throw new ArgumentNullException( "filePath" );
            if( contentType.IsBlank() )
                throw new ArgumentNullException( "contentType" );

            this.FilePath = HostingEnvironment.MapPath( filePath );
            this.ContentType = contentType;
        }

        /// <summary>
        /// Constructs a new instance of the <c>ImageResult</c> class
        /// with the image contained in the specified stream, and the image type.
        /// </summary>
        /// <param name="s">The image as a byte stream.</param>
        /// <param name="contentType">The mime type of the image.</param>
        public ImageResult( Stream s, string contentType )
            : this( s, contentType, DateTime.UtcNow )
        {
        }

        /// <summary>
        /// Constructs a new instance of the <c>ImageResult</c> class
        /// with the image contained in the specified stream, an the image type
        /// and a DateTime instance indicating when the image was last modified.
        /// </summary>
        /// <param name="s">The image as a byte stream.</param>
        /// <param name="contentType">The mime type of the image.</param>
        /// <param name="lastModified">The time the image was last modified.</param>
        public ImageResult( Stream s, string contentType, DateTime lastModified )
        {
            if( s == null )
                throw new ArgumentNullException( "s" );
            if( contentType.IsBlank() )
                throw new ArgumentNullException( "contentType" );

            this.FileStream = s;
            this.ContentType = contentType;
            this.LastModified = lastModified;
        }

        /// <summary>
        /// Renders the image and writes it to the response.
        /// </summary>
        /// <param name="context">The context of the controller in which the result is executed.</param>
        public override void ExecuteResult( ControllerContext context )
        {
            context.HttpContext.Response.ContentType = this.ContentType;

            /* ImageResult can send both streams and local files to the browser.
             * Either the stream is set, or the file path. */
            if( this.FileStream != null )
            {
                this.SendFromStream( context );
            }
            else
            {
                this.SendFromFilePath( context );
            }
        }

        /// <summary>
        /// Sends the image as a stream of bytes to the response.
        /// </summary>
        /// <param name="context">The context of the controller in which the result is executed.</param>
        private void SendFromStream( ControllerContext context )
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            /* Sending an HTTP Not Modified header when the image is already cached
             * on the client is nice for performance. */
            if( this.IsCachedOnClient( request, this.LastModified ) )
            {
                this.SetResponseToNotModified( response );
                /* No need to send any content to the client. */
                return;
            }

            /* Caching headers, so the image will be cached on the client for the next request. */
            this.SetCaching( response, this.LastModified );

            /* Read the image stream and send it to the response. */
            var outputStream = response.OutputStream;
            using( this.FileStream )
            {
                this.FileStream.Position = 0;

                var buffer = new byte[ BufferSize ];
                int bytesRead;
                while( ( bytesRead = this.FileStream.Read( buffer, 0, buffer.Length ) ) > 0 )
                {
                    outputStream.Write( buffer, 0, bytesRead );
                }
            }
        }

        /// <summary>
        /// Sends the image located on the file system to the response.
        /// </summary>
        /// <param name="context"></param>
        private void SendFromFilePath( ControllerContext context )
        {
            var request = context.HttpContext.Request;
            var response = context.HttpContext.Response;

            var fileInfo = new FileInfo( this.FilePath );
            if( !fileInfo.Exists )
            {
                response.StatusCode = 404;
                return;
            }

            /* Sending an HTTP Not Modified header when the image is already cached
             * on the client is nice for performance. */
            if( this.IsCachedOnClient( request, fileInfo.LastWriteTimeUtc ) )
            {
                this.SetResponseToNotModified( response );
                return;
            }

            /* Caching headers, so the image will be cached on the client for the next request. */
            this.SetCaching( response, fileInfo.LastWriteTime );

            /* Send the image to the response in one go. */
            response.TransmitFile( this.FilePath );
        }

        /// <summary>
        /// Sets response caching headers so the image will load faster next time.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="lastModified">The date the image was last modified.</param>
        private void SetCaching( HttpResponseBase response, DateTime lastModified )
        {
            response.Cache.SetLastModified( lastModified );
            response.Cache.SetCacheability( HttpCacheability.Public );
            response.Cache.SetValidUntilExpires( true );
        }

        /// <summary>
        /// Determines whether the image is already cached on the client.
        /// </summary>
        /// <param name="request">The client request.</param>
        /// <param name="lastModified">The date the image was last modified.</param>
        /// <returns>True if the image is already cached, false if it isn't.</returns>
        private bool IsCachedOnClient( HttpRequestBase request, DateTime lastModified )
        {
            string modifiedHeader = request.Headers[ "If-Modified-Since" ];
            if( modifiedHeader.IsBlank() )
                return false;

            var clientModified = DateTime.ParseExact( modifiedHeader, "r", CultureInfo.InvariantCulture );
            return clientModified == lastModified.AddMilliseconds( -lastModified.Millisecond );
        }

        /// <summary>
        /// Sets a HTTP 304 Not Modified header for the response.
        /// </summary>
        /// <param name="response">The response.</param>
        private void SetResponseToNotModified( HttpResponseBase response )
        {
            response.StatusCode = 304;
            response.StatusDescription = "Not Modified";
            response.SuppressContent = true;

            /* Explicitly set the Content-Length header so the client doesn't wait for
             * content but keeps the connection open for other requests. */
            response.AddHeader( "Content-Length", "0" );
        }
    }
}