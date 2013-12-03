using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IHttpRequestUnbinder<T> 
    {
        bool TryToUnbind(HttpRequest httpRequest, out T request, IList<string> messages);
    }
}
