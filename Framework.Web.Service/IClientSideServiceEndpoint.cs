using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Service.Models;

namespace Framework.Web.Service
{
    public interface IClientSideServiceEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        IServiceEndpoint<TRequest, TResponse> HttpEndpoint { get; }

        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; }
    }
}