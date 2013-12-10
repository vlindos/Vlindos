using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonResponseWritter<TRequest, TResponse> : IResponseWritter<TRequest, TResponse>
    {
    }

    public class JsonResponseWritter<TRequest, TResponse> : IJsonResponseWritter<TRequest, TResponse>
    {
        public void WriteResponse(IHttpRequest<TRequest> httpRequest, IHttpResponse<TResponse> httpResponse)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}