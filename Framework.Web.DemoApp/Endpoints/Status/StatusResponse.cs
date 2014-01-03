using System.Collections.Generic;
using Framework.Web.Service;

namespace Framework.Web.DemoApp.Endpoints.Status
{
    public class EndpointDto
    {
        public string Method { get; set; }
        public string Url { get; set; }
        public string Version { get; set; }
    }

    public class StatusResponse : ServiceResponse
    {
        public List<EndpointDto> Endpoints { get; set; } 
    }
}