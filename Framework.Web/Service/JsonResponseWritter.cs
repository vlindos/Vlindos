using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Service
{
    public interface IJsonResponseWritter<in TResponse> : IResponseWritter<TResponse>
    {
    }

    public class JsonResponseWritter<TResponse> : IJsonResponseWritter<TResponse>
    {
        public void WriteResponse(HttpContext httpContext, TResponse response)
        {
            //httpResponse.ContentType = new ContentType("application/json");
            //var js = new Newtonsoft.Json.JsonSerializer();
            //js.Serialize(httpResponse.Stream, response);
        }
    }
}