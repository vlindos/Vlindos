using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Application.HttpEndpoint.Filters;

namespace Framework.Web.HtmlPages
{
    public abstract class HtmlActionEndpointBase<TRequest, TResponse> : IServerSideHttpEndpoint<TRequest, TResponse>
    {
        public IHttpEndpoint<TRequest> HttpEndpoint { get; private set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; private set; }
        public IResponseWritter<TRequest, TResponse> ResponseWritter { get; private set; }
        public IRequestPerformer<TResponse> RequestPerformer { get; private set; }
        public List<IHttpEndpointFilter> Filters { get; private set; }
    }
}