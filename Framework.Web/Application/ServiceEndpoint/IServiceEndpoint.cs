using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.ServiceEndpoint.Models;

namespace Framework.Web.Application.ServiceEndpoint
{
    public interface IServiceEndpoint<in TRequest, out TResponse> : IHttpEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
    }
}