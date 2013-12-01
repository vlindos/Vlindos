using Framework.Web.Application.Endpoints.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IEndpoint<TRequest, TResponse>
        where TRequest : IEndpointRequest
        where TResponse : IEndpointResponse
    {
        IEndpointDescriptor<TRequest> Descriptor { get; }
        IPerformer<TRequest, TResponse> RequestPerformer { get; }
        IResponseStreamWriter<TResponse> ResponseStreamWriter { get; }
    }
}