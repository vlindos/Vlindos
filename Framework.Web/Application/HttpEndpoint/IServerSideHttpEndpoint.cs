using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint.Filters;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IServerSideHttpEndpoint
    {
        IHttpEndpoint HttpEndpoint { get; }

        IHttpRequestUnbinder HttpRequestUnbinder { get; }

        IResponseWritter ResponseWritter { get; }

        IRequestPerformer RequestPerformer { get; }

        List<IHttpEndpointFilter> Filters { get; } 
    }
}