using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IClientSideHttpEndpoint<TRequest, TResponse>
    {
        IHttpMethod[] HttpMethod { get; set; }

        IHttpEndpoint<TRequest> HttpEndpoint { get; }

        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; }
    }
}