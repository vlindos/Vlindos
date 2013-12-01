using Framework.Web.Application.Filters;

namespace Framework.Web.Application.Endpoints.Filters
{
    public interface IPreProcessingEndpointFilter : IPreProcessingFilter, IEndpointFilter
    {
    }
}