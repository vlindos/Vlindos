using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestUnbinder<T> 
    {
        bool TryToUnbind(IHttpRequest httpRequest, out T request, IList<string> messages);
    }
}
