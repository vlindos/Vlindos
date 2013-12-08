using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public class DefaultPerfomerException : IPerformerException
    {
        public void OnException<TRequest, TResponse>(
            IHttpRequest<TRequest> request, 
            IHttpResponse<TResponse> httpResponse,
            IServerSideHttpEndpoint<TRequest, TResponse> endpointDescriptor, 
            Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}