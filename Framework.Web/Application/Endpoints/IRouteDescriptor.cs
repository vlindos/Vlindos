using Framework.Web.Application.Endpoints.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IRouteDescriptor
    {
        RouteDescription RouteDescription { get; }
    }
}
