using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Authentication;
using Framework.Web.HttpMethods;
using Framework.Web.Tools;

namespace Framework.Web.DemoApp.Endpoints.Grep
{
    public class GrepRequestEndpoint : IHttpEndpoint<GrepRequest, string>
    {
        public GrepRequestEndpoint(
            IPostHttpMethod postHttpMethod, 
            IHttpRequestProcessor<IHttpEndpoint<GrepRequest, string>> httpRequestProcessor,
            IRequireAuthenticationFilter requireAuthenticationFilter,
            IGrepRequestUnbinder unbinder,
            IGrepRequestValidator validator,
            IGrepRequestPerformer performer,
            IGrepRequestFailureHandler stringFailureHandler,
            IStringResponseWritter stringResponseWritter)
        {
            HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = postHttpMethod,
                RouteDescription = "grep/{for}"
            };
            HttpRequestProcessor = httpRequestProcessor;
            BeforePerformActions = new List<IPrePerformAction> { requireAuthenticationFilter };
            ResponseWritter = stringResponseWritter;
            HttpRequestUnbinder = unbinder;
            Performer = performer;
            RequestValidator = validator;
            RequestFailureHandler = stringFailureHandler;
        }

        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor<IHttpEndpoint<GrepRequest, string>> HttpRequestProcessor { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<GrepRequest, string> Performer { get; set; }
        public IResponseWritter<string> ResponseWritter { get; set; }
        public IHttpRequestUnbinder<GrepRequest> HttpRequestUnbinder { get; set; }
        public IRequestValidator<GrepRequest> RequestValidator { get; set; }
        public IRequestFailureHandler<GrepRequest, string> RequestFailureHandler { get; set; }
    }
}