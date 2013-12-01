using System.Collections.Generic;
using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IResponseStreamReader<T> 
        where T : IEndpointResponse
    {
        bool Read(HttpRequest httpRequest, out T response, List<string> messages);
    }
}