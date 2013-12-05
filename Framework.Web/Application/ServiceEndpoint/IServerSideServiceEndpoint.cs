using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.ServiceEndpoint.Models;

namespace Framework.Web.Application.ServiceEndpoint
{
    public interface IServerSideServiceEndpoint<TRequest, TResponse> : IServerSideHttpEndpointDescriptor
        where TResponse : IServiceResponse
    {
        IServiceEndpoint<TRequest, TResponse> ServiceEndpoint { get; }

        IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; }

        IHttpStreamResponseWritter<TResponse> HttpStreamResponseWritter { get; }

        IRequestPerformer<TResponse> RequestPerformer { get; }
    }
}