using System.Collections.Generic;
using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IHttpRequestBuilder<T> where T : IEndpointRequest
    {
        bool Build(out HttpRequest httpRequest, List<string> messages);
    }
}
