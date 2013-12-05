using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Application
{
    public interface IApplicationDelegate<TEndpoint, TRequest, TResponse>
        where TEndpoint : IServerSideHttpEndpoint<TRequest, TResponse>
    {
        bool Start(Models.Application<TEndpoint, TRequest, TResponse> application);
        bool Shutdown(Models.Application<TEndpoint, TRequest, TResponse> application);
    }
}