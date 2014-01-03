using System.Collections.Generic;
using Framework.Web.Application;

namespace Framework.Web.Service
{
    public interface IHttpStreamResponseReader<TResponse>
    {
        bool Read(HttpContext httpContext, List<string> messages, out TResponse response);
    }
}