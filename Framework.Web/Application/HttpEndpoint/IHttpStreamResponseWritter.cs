using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpStreamResponseWritter<in T>
    {
        void WriteResponse(IHttpRequest httpRequest, HttpResponse httpResponse, T response);
    }
}
