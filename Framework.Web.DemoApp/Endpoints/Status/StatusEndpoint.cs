using System.Collections.Generic;
using System.Linq;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Service;

namespace Framework.Web.DemoApp.Endpoints.Status
{
    public class StatusEndpoint : ServiceEndpointBase<StatusResponse>
    {
        public StatusEndpoint(IServiceEndpointBootstrapper<StatusResponse> bootstrapper, IEnumerable<IHttpEndpoint> httpEndpoints)
        {
            bootstrapper.Bootstrap(this, "status", httpRequest => new StatusResponse
                {
                    Success = true,
                    Endpoints = httpEndpoints.Select(x => new EndpointDto
                    {
                        Method = x.HttpRequestDescriptor.HttpMethod.MethodName,
                        Url = x.HttpRequestDescriptor.RouteDescription,
                        Version = x.GetType().Assembly.GetName().Version.ToString()
                    }).ToList()
                });
        }
    }
}
