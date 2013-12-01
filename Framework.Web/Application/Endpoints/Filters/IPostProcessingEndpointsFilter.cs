using Framework.Web.Application.Filters;

namespace Framework.Web.Application.Endpoints.Filters
{
    public interface IPostProcessingEndpointsFilter : IPostProcessingFilter, IEndpointFilter
    {
    }
}