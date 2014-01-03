using Framework.Web.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public class GenericRequestDescriptor : IHttpRequestDescriptor
    {
        public IHttpMethod HttpMethod { get; set; }
        public string RouteDescription { get; set; }
    }
}