using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IResponseStreamReader<T> 
    {
        bool Read(HttpRequest httpRequest, out T response, List<string> messages);
    }
}