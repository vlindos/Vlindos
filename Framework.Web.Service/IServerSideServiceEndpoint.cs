using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Service.Models;

namespace Framework.Web.Service
{
    public interface IServerSideServiceEndpoint<TRequest, TResponse> 
        : IServerSideHttpEndpoint<TRequest, TResponse> 
        where TResponse : IServiceResponse
    {
        IServiceEndpoint<TRequest, TResponse> ServiceEndpoint { get; }
    }
}