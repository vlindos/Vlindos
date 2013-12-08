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
            IJsonHttpStreamResponseWritter<int, ServiceResponse> jsonHttpStreamResponseWritter)
        {
            Filters = new List<IHttpEndpointFilter> { serviceRequestHandler };
            HttpMethods = new IHttpMethod[] { postHttpMethod };
            HttpEndpoint = httpEndpoint;
            RequestPerformer = requestPerformer;
            HttpRequestUnbinder = httpRequestUnbinder;
            HttpStreamResponseWritter = jsonHttpStreamResponseWritter;
        }

        public IHttpMethod[] HttpMethods { get; set; }
        public IHttpEndpoint<int> HttpEndpoint { get; private set; }
        public IHttpRequestUnbinder<int> HttpRequestUnbinder { get; private set; }
        public IHttpStreamResponseWritter<int, ServiceResponse> HttpStreamResponseWritter { get; private set; }
        public IRequestPerformer<ServiceResponse> RequestPerformer { get; private set; }
        public List<IHttpEndpointFilter> Filters { get; private set; }
    }
}
