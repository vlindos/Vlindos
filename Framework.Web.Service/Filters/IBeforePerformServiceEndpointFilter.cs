using Framework.Web.Application.Filters;
using Framework.Web.Models;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Filters
{
    public interface IBeforePerformServiceEndpointFilter<TRequest, TResponse> 
        : IBeforePerformFilter, IServiceEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        bool BeforePerform(
            IHttpRequest request, IHttpResponse httpResponse, IServerSideServiceEndpoint<TRequest, TResponse> httpEndpoint);
    }
}