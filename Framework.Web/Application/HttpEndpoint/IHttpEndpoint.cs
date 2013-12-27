using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpEndpoint
    {
        IHttpMethod[] HttpMethods { get; set; }

        string RouteDescription { get; }
    }
}