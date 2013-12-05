using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IPerformerException
    {
        void OnException<TEndpoint, TRequest, TResponse>(
                         IHttpRequest request, 
                         IHttpResponse httpResponse,
                         TEndpoint endpointDescriptor, 
                         Exception exception)
            where TEndpoint : IServerSideHttpEndpoint<TRequest, TResponse>;
    }
}
