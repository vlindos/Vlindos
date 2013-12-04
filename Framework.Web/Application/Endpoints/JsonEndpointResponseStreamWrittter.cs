using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IJsonEndpointResponseStreamWriter<in T> : IResponseStreamWriter<T>
    {
    }

    public class JsonEndpointResponseStreamWriter<T> : IJsonEndpointResponseStreamWriter<T>
    {
        public void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse, T response)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}