using System.Collections.Generic;

namespace Users.Common.Models.Endpoint
{
    public interface IHttpRequestBuilder<T> where T : IEndpointRequest
    {
        bool Build(out IHttpRequest httpRequest, List<string> messages);
    }
}
