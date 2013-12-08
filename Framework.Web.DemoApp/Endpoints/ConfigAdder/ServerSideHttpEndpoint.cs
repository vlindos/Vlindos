using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Service;
using Framework.Web.Service.Filters;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public class ServerSideHttpEndpoint : IServerSideHttpEndpoint<int, ServiceResponse>
    {
        public ServerSideHttpEndpoint(
            IHttpEndpoint httpEndpoint,
            IServiceRequestHandler<int, ServiceResponse> serviceRequestHandler, 
            IRequestPerformer requestPerformer,
            IHttpRequestUnbinder httpRequestUnbinder,
            IJsonHttpStreamResponseWritter<ServiceResponse> jsonHttpStreamResponseWritter)
        {
            Filters = new List<IHttpEndpointFilter<int, ServiceResponse>> { serviceRequestHandler };
            HttpEndpoint = httpEndpoint;
            RequestPerformer = requestPerformer;
            HttpRequestUnbinder = httpRequestUnbinder;
            HttpStreamResponseWritter = jsonHttpStreamResponseWritter;
        }

        public IHttpEndpoint<int> HttpEndpoint { get; private set; }
        public IHttpRequestUnbinder<int> HttpRequestUnbinder { get; private set; }
        public IHttpStreamResponseWritter<ServiceResponse> HttpStreamResponseWritter { get; private set; }
        public IRequestPerformer<ServiceResponse> RequestPerformer { get; private set; }
        public List<IHttpEndpointFilter<int, ServiceResponse>> Filters { get; private set; }
    }
}
