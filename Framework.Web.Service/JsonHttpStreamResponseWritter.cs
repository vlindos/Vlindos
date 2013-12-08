using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonHttpStreamResponseWritter<TRequest, TResponse> : IHttpStreamResponseWritter<TRequest, TResponse>
    {
    }

    public class JsonHttpStreamResponseWritter<TRequest, TResponse> : IJsonHttpStreamResponseWritter<TRequest, TResponse>
    {
        public void WriteResponse(IHttpRequest<TRequest> httpRequest, HttpResponse<TResponse> httpResponse)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}