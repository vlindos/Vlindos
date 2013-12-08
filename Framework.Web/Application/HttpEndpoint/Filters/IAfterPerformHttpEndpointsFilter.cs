using Framework.Web.Application.Filters;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint.Filters
{
    public interface IAfterPerformHttpEndpointsFilter<TRequest, TResponse> 
        : IAfterPerformFilter, IHttpEndpointFilter<TRequest, TResponse>
    {
        bool AfterPerform(
            IHttpRequest<TRequest> httpRequest, 
            IHttpResponse<TResponse> httpResponse, 
            IServerSideHttpEndpoint<TRequest, TResponse> httpEndpoint);
    }
}