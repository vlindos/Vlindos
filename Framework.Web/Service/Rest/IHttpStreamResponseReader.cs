using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Service.Rest
{
    public interface IHttpStreamResponseReader<TResponse>
    {
        bool Read(HttpResponse httpRequest, List<string> messages, out TResponse response);
    }
}