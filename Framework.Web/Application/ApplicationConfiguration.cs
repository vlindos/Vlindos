using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Application
{
    public class ApplicationConfiguration
    {
        public IHttpException HttpException { get; set; }
        public List<IHttpEndpoint> HttpEndpoints { get; set; }
    }
}