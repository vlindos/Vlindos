namespace Framework.Web.Application.HttpEndpoint
{
    public interface IClientSideHttpEndpoint<TRequest, TResponse>
    {
        IHttpEndpoint<TRequest, TResponse> HttpEndpoint { get; }

        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; }
    }
}