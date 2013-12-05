using Framework.Web.Application.Filters;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint.Filters
{
    public interface IAfterPerformHttpEndpointsFilter<TRequest, TResponse> 
        : IAfterPerformFilter, IHttpEndpointFilter<TRequest, TResponse>
    {
        bool AfterPerform(IHttpRequest request, IHttpResponse httpResponse, IServerSideHttpEndpoint<TRequest, TResponse> httpEndpoint);
    }
}