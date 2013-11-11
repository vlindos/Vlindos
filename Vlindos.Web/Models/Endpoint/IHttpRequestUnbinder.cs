using System.Collections.Generic;
using System.Web.Mvc;

namespace Users.Common.Models.Endpoint
{
    public interface IHttpRequestUnbinder<T> where T : IEndpointRequest
    {
        bool TryToUnbind(ControllerContext controllerContext, out T request, IList<string> messages);
    }
}
