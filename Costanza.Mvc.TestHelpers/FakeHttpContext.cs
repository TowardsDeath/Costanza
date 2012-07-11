using System;
using System.Collections.Specialized;
using System.IO;
using System.Web;

using Moq;

namespace Costanza.Mvc.TestHelpers
{
    public static class FakeHttpContext
    {
        public static HttpContextBase New()
        {
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var writer = new Mock<TextWriter>();

            response.Setup( r => r.Output ).Returns( writer.Object );
            response.Setup( r => r.OutputStream ).Returns( new MemoryStream() );

            context.Setup( ctx => ctx.Request ).Returns( request.Object );
            context.Setup( ctx => ctx.Response ).Returns( response.Object );
            context.Setup( ctx => ctx.Session ).Returns( session.Object );
            context.Setup( ctx => ctx.Server ).Returns( server.Object );

            return context.Object;
        }

        public static HttpContextBase New( string virtualPath )
        {
            var httpContext = FakeHttpContext.New();
            httpContext.Request.SetupRequestUrl( virtualPath );
            return httpContext;
        }

        public static HttpContextBase New( string virtualPath, string httpMethod )
        {
            var httpContext = FakeHttpContext.New();
            httpContext.Request.SetupRequestUrl( virtualPath );
            httpContext.Request.SetHttpMethodResult( httpMethod );
            return httpContext;
        }

        public static void SetHttpMethodResult( this HttpRequestBase request, string httpMethod )
        {
            Mock.Get( request )
                .Setup( req => req.HttpMethod )
                .Returns( httpMethod );
        }

        public static void SetupRequestUrl( this HttpRequestBase request, string url )
        {
            if( url == null ) throw new ArgumentNullException( "url" );
            if( !url.StartsWith( "~/" ) ) throw new ArgumentException( "Sorry, we expect a virtual url starting with \"~/\"." );

            var mock = Mock.Get( request );

            mock.Setup( req => req.QueryString ).Returns( GetQueryStringParameters( url ) );
            mock.Setup( req => req.AppRelativeCurrentExecutionFilePath ).Returns( GetUrlFileName( url ) );
            mock.Setup( req => req.PathInfo ).Returns( "" );
        }

        static NameValueCollection GetQueryStringParameters( string url )
        {
            if( !url.Contains( "?" ) ) return null;

            string[] parts = url.Split( "?".ToCharArray() );
            string[] keys = parts[ 1 ].Split( "&".ToCharArray() );

            var parameters = new NameValueCollection();
            foreach( string key in keys )
            {
                string[] part = key.Split( "=".ToCharArray() );
                parameters.Add( part[ 0 ], part[ 1 ] );
            }
            return parameters;
        }

        static string GetUrlFileName( string url )
        {
            if( url.Contains( "?" ) )
            {
                return url.Substring( 0, url.IndexOf( "?" ) );
            }
            else
            {
                return url;
            }
        }
    }
}