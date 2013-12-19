using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;

namespace Framework.Web.HtmlPages
{
    public abstract class HtmlPageEndpointBase<TRequest, TResponse> : IServerSideHttpEndpoint<TRequest, TResponse>
    {
        public IHttpEndpoint<TRequest> HttpEndpoint { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IResponseWritter<TRequest, TResponse> ResponseWritter { get; set; }
        public IRequestPerformer<TResponse> RequestPerformer { get; set; }
        public List<IHttpEndpointFilter> Filters { get; set; }
    }
}
