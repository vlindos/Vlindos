using System.Collections.Generic;
using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestUnbinder<TRequest> 
    {
        bool TryToUnbind(IHttpRequest<TRequest> httpRequest, IList<string> messages);
    }
}
