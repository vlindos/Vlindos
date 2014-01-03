using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestUnbinder<TRequest>
    {
        bool TryToUnbind(HttpRequest httpRequest, IList<string> messages, out TRequest request);
    }
}
