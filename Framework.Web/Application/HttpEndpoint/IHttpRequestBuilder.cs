using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestBuilder
    {
        bool Build(HttpRequest httpRequest, List<string> messages);
    }
}
