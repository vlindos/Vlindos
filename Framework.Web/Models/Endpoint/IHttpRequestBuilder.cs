using System.Collections.Generic;

namespace Vlindos.Web.Models.Endpoint
{
    public interface IHttpRequestBuilder<T> where T : IEndpointRequest
    {
        bool Build(out IHttpRequest httpRequest, List<string> messages);
    }
}
