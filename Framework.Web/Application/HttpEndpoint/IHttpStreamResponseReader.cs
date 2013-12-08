using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpStreamResponseReader<TResponse> 
    {
        bool Read(IHttpResponse<TResponse> httpRequest, List<string> messages);
    }
}