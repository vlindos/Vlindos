using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public class DefaultPerfomerException : IPerformerException
    {
        public void OnException(
            HttpRequest request, 
            HttpResponse httpResponse,
            IServerSideHttpEndpoint endpointDescriptor, 
            Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}