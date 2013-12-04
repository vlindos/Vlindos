using Framework.Web.Application.Endpoints.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IEndpoint<TRequest, TResponse>
    {
        RouteDescription RouteDescription { get; }
        IHttpRequestBuilder<TRequest> RequestBuilder { get; }
        IRequestValidator<TRequest> RequestValidator { get; }
        IHttpRequestUnbinder<TRequest> RequestUnbinder { get; }
        IPerformer<TRequest, TResponse> Performer { get; }
        IResponseStreamWriter<TResponse> ResponseStreamWriter { get; }
        IResponseStreamReader<TResponse> ResponseStreamReader { get; }
    }
}