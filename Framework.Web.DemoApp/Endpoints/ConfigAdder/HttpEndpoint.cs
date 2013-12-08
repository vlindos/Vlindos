using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
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
            HttpMethods = new IHttpMethod[] { postHttpMethod };
            HttpUrlDescription = "/Config/Add";

            RequestValidator = requestValidator;
        }

        public IHttpMethod[] HttpMethods { get; set; }
        public string HttpUrlDescription { get; private set; }

        public IRequestValidator<int> RequestValidator { get; private set; }
    }
}