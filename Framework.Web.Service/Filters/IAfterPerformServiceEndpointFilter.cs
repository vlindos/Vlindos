using Framework.Web.Application.Filters;
using Framework.Web.Models;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Filters
{
    public interface IAfterPerformServiceEndpointFilter<TRequest, TResponse> 
        : IAfterPerformFilter, IServiceEndpointFilter
        where TResponse : IServiceResponse
    {
        bool BeforePerform(
            IHttpRequest request, IHttpResponse httpResponse, IServerSideServiceEndpoint<TRequest, TResponse> httpEndpoint);
    }
}