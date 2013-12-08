using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Models;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public interface IHttpEndpoint : IHttpEndpoint<int>
    {
    }

    public class HttpEndpoint : IHttpEndpoint
    {
        public HttpEndpoint(IPostHttpMethod postHttpMethod, IRequestValidator requestValidator)
        {
            HttpUrlDescription = new HttpUrlDescription
            {
                HttpMethods = new IHttpMethod[] { postHttpMethod },
                Path = "/Config/Add"
            };

            RequestValidator = requestValidator;
        }
        public HttpUrlDescription HttpUrlDescription { get; private set; }
        public IRequestValidator<int> RequestValidator { get; private set; }
    }
}