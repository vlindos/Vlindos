using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpStreamResponseWritter<TRequest, TResponse>
    {
        void WriteResponse(IHttpRequest<TRequest> httpRequest, HttpResponse<TResponse> httpResponse);
    }
}
