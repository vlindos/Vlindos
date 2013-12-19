using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IResponseWritter
    {
        void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse);
    }
}
