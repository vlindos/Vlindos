using Framework.Web.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestDescriptor
    {
        // Any IHttpMethod derivate.
        // E.g. IGetHttpMethod
        IHttpMethod HttpMethod { get; set; }

        // Raw url including paraemetrized paths
        // E.g. "Help/{applicationName}"
        string RouteDescription { get; set; } 
    }
}