using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestBuilder<T>
    {
        bool Build(out IHttpRequest httpRequest, List<string> messages);
    }
}
