using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.HttpMethods;
using Framework.Web.Tools;

namespace Framework.Web.JavaScript
{
    public interface IClientValidationsEndpoint : IHttpEndpoint<string>
    {
    }

    public class ClientValidationsEndpoint : IClientValidationsEndpoint
    {
        public ClientValidationsEndpoint(
            IStringResponseWritter stringResponseWritter, 
            IJavascriptSourcePerformer javascriptSourcePerformer,
            IStandardHttpRequestProcessor httpRequestProcessor, 
            IGetHttpMethod getHttpMethod)
        {
            HttpRequestDescriptor = new GenericRequestDescriptor
            {
                HttpMethod = getHttpMethod,
                RouteDescription = "assets/javascript/service-validation.js"
            };
            HttpRequestProcessor = httpRequestProcessor;
            Performer = javascriptSourcePerformer;
            ResponseWritter = stringResponseWritter;
        }

        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<string> Performer { get; set; }
        public IResponseWritter<string> ResponseWritter { get; set; }
    }
}
