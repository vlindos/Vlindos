using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public interface IHttpEndpoint : IHttpEndpoint<int>
    {
    }

    public class HttpEndpoint : IHttpEndpoint
    {
        public HttpEndpoint(IRequestValidator requestValidator)
        {
            HttpUrlDescription = "/Config/Add";

            RequestValidator = requestValidator;
        }
        public string HttpUrlDescription { get; private set; }
        public IRequestValidator<int> RequestValidator { get; private set; }
    }
}