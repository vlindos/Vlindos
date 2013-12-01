using System.Net.Mime;
using Framework.Web.Application.Endpoints.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.Endpoints
{
    public interface IJsonEndpointResponseStreamWriter<in T> : IResponseStreamWriter<T>
        where T : IEndpointResponse
    {
    }

    public class JsonEndpointResponseStreamWriter<T> : IJsonEndpointResponseStreamWriter<T>
        where T : IEndpointResponse
    {
        public void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse, T response)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}