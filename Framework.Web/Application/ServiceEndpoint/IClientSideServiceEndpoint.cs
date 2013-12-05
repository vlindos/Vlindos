using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.ServiceEndpoint.Models;

namespace Framework.Web.Application.ServiceEndpoint
{
    public interface IClientSideServiceEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        IServiceEndpoint<TRequest, TResponse> HttpEndpoint { get; }

        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; }
    }
}