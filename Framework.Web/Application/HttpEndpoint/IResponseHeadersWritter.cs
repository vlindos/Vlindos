namespace Framework.Web.Application.HttpEndpoint
{
    public interface IResponseHeadersWritter
    {
        void WriteResponseHeaders(HttpContext httpContext);
    }
}