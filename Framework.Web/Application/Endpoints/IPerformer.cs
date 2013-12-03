using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IPerformer<in TRequest, out TResponse>
        where TResponse : IEndpointResponse
    {
        TResponse Perform(HttpRequest httpRequest, TRequest request);
    }
}