using System;
using System.IO;
using System.Web.Mvc;

namespace Costanza.Mvc
{
    public class ImageResult : ActionResult
    {
        protected readonly string FilePath;

        public ImageResult( string filePath )
        {
            this.FilePath = filePath;
        }

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
            // Caching headers
            response.Cache.SetETagFromFileDependencies();
            response.AddHeader( "Last-Modified", fileInfo.LastWriteTime.ToString( "r" ) );
            response.Cache.SetValidUntilExpires( true );
            response.Cache.SetMaxAge( TimeSpan.FromSeconds( 60 * 10 ) );

            response.WriteFile( this.FilePath );
        }

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