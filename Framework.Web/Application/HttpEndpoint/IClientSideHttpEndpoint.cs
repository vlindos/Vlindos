namespace Framework.Web.Application.HttpEndpoint
{
    public interface IClientSideHttpEndpoint
    {
        IHttpEndpoint HttpEndpoint { get; }

        IHttpRequestBuilder HttpRequestBuilder { get; }

        IHttpStreamResponseReader HttpStreamResponseReader { get; }
    }
}