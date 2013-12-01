using System.Collections.Generic;
using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IHttpRequestUnbinder<T> where T : IEndpointRequest
    {
        bool TryToUnbind(HttpRequest httpRequest, out T request, IList<string> messages);
    }
}
