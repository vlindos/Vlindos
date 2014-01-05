using System.Collections.Generic;
using Framework.Web.Application;

namespace Framework.Web.HttpEndpoint
{
    public interface IHttpRequestBuilder<in TRequest>
    {
        bool Build(TRequest request, List<string> messages, out HttpRequest httpRequest);
    }
}
