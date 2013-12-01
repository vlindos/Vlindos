using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IResponseStreamWriter<in T>
        where T : IEndpointResponse
    {
        void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse, T response);
    }
}
