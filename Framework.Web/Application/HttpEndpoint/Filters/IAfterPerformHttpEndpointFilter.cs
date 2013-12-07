using Framework.Web.Application.Filters;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint.Filters
{
    public interface IAfterPerformHttpEndpointFilter<TRequest, TResponse> 
        : IAfterPerformFilter, IHttpEndpointFilter<TRequest, TResponse>
    {
        bool BeforePerform(
            IHttpRequest request, IHttpResponse httpResponse, IServerSideHttpEndpoint<TRequest, TResponse> httpEndpoint);
    }
}