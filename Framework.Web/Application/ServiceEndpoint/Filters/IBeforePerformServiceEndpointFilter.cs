using Framework.Web.Application.Filters;
using Framework.Web.Application.ServiceEndpoint.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.ServiceEndpoint.Filters
{
    public interface IBeforePerformServiceEndpointFilter<TRequest, TResponse> 
        : IBeforePerformFilter, IServiceEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        bool BeforePerform(
            IHttpRequest request, IHttpResponse httpResponse, IServerSideServiceEndpoint<TRequest, TResponse> httpEndpoint);
    }
}