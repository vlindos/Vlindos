namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestProcessor
    {
        void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint httpEndpoint);
    }
}
