using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;

namespace Framework.Web.HtmlPages
{
    public abstract class HtmlPageEndpointBase<TRequest> : IServerSideHttpEndpoint<TRequest>
    {
        public IHttpEndpoint<TRequest> HttpEndpoint { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IResponseWritter ResponseWritter { get; set; }
        public IRequestPerformer RequestPerformer { get; set; }
        public List<IHttpEndpointFilter> Filters { get; set; }
    }
}
