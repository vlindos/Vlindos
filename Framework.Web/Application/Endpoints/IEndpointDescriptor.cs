using Framework.Web.Application.Endpoints.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IEndpointDescriptor<T>
    {
        RouteDescription RouteDescription { get; }
        IHttpRequestBuilder<T> RequestBuilder { get; }
        IRequestValidator<T> RequestValidator { get; }
        IHttpRequestUnbinder<T> RequestUnbinder { get; }
    }
}
