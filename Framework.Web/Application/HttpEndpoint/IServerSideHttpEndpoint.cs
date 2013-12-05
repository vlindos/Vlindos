namespace Framework.Web.Application.HttpEndpoint
{
    public interface IServerSideHttpEndpoint<TRequest, TResponse> : IServerSideHttpEndpointDescriptor
    {
        IHttpEndpoint<TRequest, TResponse> HttpEndpoint { get; }

        IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; }

        IHttpStreamResponseWritter<TResponse> HttpStreamResponseWritter { get; }

        IRequestPerformer<TResponse> RequestPerformer { get; }
    }
}