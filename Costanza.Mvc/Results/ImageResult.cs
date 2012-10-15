using System;
using System.IO;
using System.Web.Mvc;

namespace Costanza.Mvc
{
    /// <summary>
    /// Action result that writes a web image to the response.
    /// </summary>
    public class ImageResult : ActionResult
    {
        /// <summary>
        /// The path to the image on the local file system.
        /// </summary>
        protected readonly string FilePath;

        /// <summary>
        /// Initializes a new instance of the <c>ImageResult</c> class 
        /// with the specified image file path.
        /// </summary>
        /// <param name="filePath">The path to the image on the local file system.</param>
        public ImageResult( string filePath )
        {
            this.FilePath = filePath;
        }

        /// <summary>
        /// Locates the image and writes it to the response.
        /// </summary>
        /// <param name="context">The context of the controller in which the result is executed.</param>
        public override void ExecuteResult( ControllerContext context )
        {
            var fileInfo = new FileInfo( this.FilePath );
            if( !fileInfo.Exists )
            {
                context.HttpContext.Response.StatusCode = 404;
                return;
            }

            var response = context.HttpContext.Response;
            response.ContentType = this.GetContentType( fileInfo.Extension );
            /* Caching headers */
            response.Cache.SetETagFromFileDependencies();
            response.AddHeader( "Last-Modified", fileInfo.LastWriteTime.ToString( "r" ) );
            response.Cache.SetValidUntilExpires( true );
            response.Cache.SetMaxAge( TimeSpan.FromSeconds( 60 * 10 ) );

            response.WriteFile( this.FilePath );
        }

        /// <summary>
        /// Gets the MIME type by checking the specified file extension.
        /// </summary>
        /// <param name="extension">The file extension to check.</param>
        /// <returns>A string containing the MIME type.</returns>
        protected string GetContentType( string extension )
        {
            switch( extension )
            {
                case ".jpg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";
                default:
                    throw new NotSupportedException( "This type of image is not supported as an ImageResult: {0}.".FormatWith( extension ) );
            }
        }
    }
}