using Framework.Web.Models;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IResponseWritter<TRequest, TResponse>
    {
        void WriteResponse(IHttpRequest<TRequest> httpRequest, IHttpResponse<TResponse> httpResponse);
    }
}
