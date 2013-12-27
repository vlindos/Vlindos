using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Rest.Models
{
    public abstract class RestEndpointBase<TRequest> : IServerSideHttpEndpoint<TRequest>
    {
        public IHttpMethod[] HttpMethods { get; set; }
        public IHttpEndpoint<TRequest> HttpEndpoint { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IResponseWritter ResponseWritter { get; set; }
        public IRequestPerformer RequestPerformer { get; set; }
        public List<IHttpEndpointFilter> Filters { get; set; }
    }
}
