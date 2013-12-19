using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.Service
{
    public interface IJsonResponseWritter: IResponseWritter
    {
    }

    public class JsonResponseWritter : IJsonResponseWritter
    {
        public void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}