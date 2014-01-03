using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint.Client
{
    public interface IHttpRequestBuilder<in TRequest>
    {
        bool Build(TRequest request, List<string> messages, out HttpRequest httpRequest);
    }
}
