using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestBuilder<TRequest>
    {
        bool Build(out IHttpRequest<TRequest> httpRequest, List<string> messages);
    }
}
