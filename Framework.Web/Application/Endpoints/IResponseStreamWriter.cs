using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IResponseStreamWriter<in T>
    {
        void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse, T response);
    }
}
