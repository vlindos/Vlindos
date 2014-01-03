using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestUnbinder<TRequest>
    {
        bool TryToUnbind(HttpRequest httpRequest, List<string> messages, out TRequest request);
    }
}
