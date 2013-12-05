using System;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.DemoApp
{
    public class ApplicationDelegate<TEndpoint, TRequest, TResponse> 
        : IApplicationDelegate<TEndpoint, TRequest, TResponse>
        where TEndpoint : IServerSideHttpEndpoint<TRequest, TResponse>
    {
        public bool Start(Web.Models.Application<TEndpoint, TRequest, TResponse> application)
        {
            throw new NotImplementedException();
        }

        public bool Shutdown(Web.Models.Application<TEndpoint, TRequest, TResponse> application)
        {
            throw new NotImplementedException();
        }
    }
}
