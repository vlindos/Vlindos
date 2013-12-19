using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpStreamResponseReader
    {
        bool Read(HttpResponse httpRequest, List<string> messages);
    }
}