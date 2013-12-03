using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IHttpRequestBuilder<T>
    {
        bool Build(out HttpRequest httpRequest, List<string> messages);
    }
}
