using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpStreamResponseReader<T> 
    {
        bool Read(IHttpRequest httpRequest, out T response, List<string> messages);
    }
}