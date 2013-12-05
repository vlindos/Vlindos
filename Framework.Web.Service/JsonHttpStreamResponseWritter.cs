using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonHttpStreamResponseWritter<in T> : IHttpStreamResponseWritter<T>
    {
    }

    public class JsonHttpStreamResponseWritter<T> : IJsonHttpStreamResponseWritter<T>
    {
        public void WriteResponse(IHttpRequest httpRequest, HttpResponse httpResponse, T response)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}