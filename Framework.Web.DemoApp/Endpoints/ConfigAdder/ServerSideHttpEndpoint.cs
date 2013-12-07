using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Service.Filters;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.ConfigAdder
{
    public class ServerSideHttpEndpoint : IServerSideHttpEndpoint<int, IServiceResponse>
    {
        public ServerSideHttpEndpoint(
            IHttpEndpoint httpEndpoint,
            IServiceRequestHandler<int, IServiceResponse> serviceRequestHandler, 
            IRequestPerformer requestPerformer)
        {
            Filters = new List<IHttpEndpointFilter<int, IServiceResponse>>
            {
                serviceRequestHandler
            };
            HttpEndpoint = httpEndpoint;
            RequestPerformer = requestPerformer;
        }

        public IHttpEndpoint<int> HttpEndpoint { get; private set; }
        public IHttpRequestUnbinder<int> HttpRequestUnbinder { get; private set; }
        public IHttpStreamResponseWritter<IServiceResponse> HttpStreamResponseWritter { get; private set; }
        public IRequestPerformer<IServiceResponse> RequestPerformer { get; private set; }
        public List<IHttpEndpointFilter<int, IServiceResponse>> Filters { get; private set; }
    }
}
