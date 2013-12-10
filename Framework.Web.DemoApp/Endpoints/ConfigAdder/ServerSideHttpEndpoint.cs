using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service;
using Framework.Web.Service.Filters;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public class ServerSideHttpEndpoint : IServerSideHttpEndpoint<int, ServiceResponse>
    {
        public ServerSideHttpEndpoint(
            IPostHttpMethod postHttpMethod,
            IHttpEndpoint httpEndpoint,
            IServiceRequestHandler serviceRequestHandler, 
            IRequestPerformer requestPerformer,
            IHttpRequestUnbinder httpRequestUnbinder,
            IJsonResponseWritter<int, ServiceResponse> jsonResponseWritter)
        {
            Filters = new List<IHttpEndpointFilter> { serviceRequestHandler };
            HttpMethods = new IHttpMethod[] { postHttpMethod };
            HttpEndpoint = httpEndpoint;
            RequestPerformer = requestPerformer;
            HttpRequestUnbinder = httpRequestUnbinder;
            ResponseWritter = jsonResponseWritter;
        }

        public IHttpMethod[] HttpMethods { get; set; }
        public IHttpEndpoint<int> HttpEndpoint { get; private set; }
        public IHttpRequestUnbinder<int> HttpRequestUnbinder { get; private set; }
        public IResponseWritter<int, ServiceResponse> ResponseWritter { get; private set; }
        public IRequestPerformer<ServiceResponse> RequestPerformer { get; private set; }
        public List<IHttpEndpointFilter> Filters { get; private set; }
    }
}
