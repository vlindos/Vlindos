using Framework.Web.Application.HttpEndpoint;
using Framework.Web.DemoApp.Endpoints.Base;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Models;

namespace Framework.Web.DemoApp.Endpoints.Sample
{
    public interface ISampleRestEndpoint : IServerSideHttpEndpoint<string, ServiceResponse>
    {
    }

    public class SampleRestEndpoint : RestEndpointBase<string, ServiceResponse>, ISampleRestEndpoint
    {
        public SampleRestEndpoint(IRestEndpointBootstrapper bootstrapper, IGetHttpMethod getHttpMethod)
        {
            bootstrapper.Bootstrap(this, getHttpMethod, "addString/{0}", null);
            RequestPerformer = null;
            HttpRequestUnbinder = null;
        }
    }
}