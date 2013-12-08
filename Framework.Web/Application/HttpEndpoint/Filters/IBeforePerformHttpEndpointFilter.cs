using Framework.Web.Application.Filters;
namespace Framework.Web.Application.HttpEndpoint.Filters
{
    public interface IBeforePerformHttpEndpointFilter : IBeforePerformFilter, IHttpEndpointFilter
    {
    }
}