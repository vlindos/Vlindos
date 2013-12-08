namespace Framework.Web.Application.HttpEndpoint
{
    public interface IClientSideHttpEndpoint<TRequest, TResponse>
    {
        IHttpEndpoint<TRequest> HttpEndpoint { get; }

        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; }
    }
}