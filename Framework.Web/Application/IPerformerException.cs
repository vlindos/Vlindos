using System;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IPerformerException
    {
        void OnException(HttpRequest request, 
                         HttpResponse httpResponse,
                         IServerSideHttpEndpoint endpointDescriptor, 
                         Exception exception);
    }
}
