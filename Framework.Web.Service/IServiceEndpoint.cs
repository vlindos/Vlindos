using Framework.Web.Service.Models;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Service
{
    public interface IServiceEndpoint<in TRequest, out TResponse> : IHttpEndpoint<TRequest>
        where TResponse : IServiceResponse
    {
    }
}