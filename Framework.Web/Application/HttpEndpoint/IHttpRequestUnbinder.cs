using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestUnbinder 
    {
        bool TryToUnbind<TRequest>(
            HttpRequest httpRequest, IList<string> messages, out TRequest request);
    }
}
