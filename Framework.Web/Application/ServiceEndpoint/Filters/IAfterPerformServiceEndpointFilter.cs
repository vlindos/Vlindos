using Framework.Web.Application.Filters;
using Framework.Web.Application.ServiceEndpoint.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.ServiceEndpoint.Filters
{
    public interface IAfterPerformServiceEndpointFilter<TRequest, TResponse> 
        : IAfterPerformFilter, IServiceEndpointFilter
        where TResponse : IServiceResponse
    {
        bool BeforePerform(
            IHttpRequest request, IHttpResponse httpResponse, IServerSideServiceEndpoint<TRequest, TResponse> httpEndpoint);
    }
}