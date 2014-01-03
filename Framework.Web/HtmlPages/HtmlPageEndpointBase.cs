using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.HtmlPages
{
    public abstract class HtmlPageEndpointBase<THtmlPageViewData>
        : IHtmlPageEndpoint<THtmlPageViewData>
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<IHtmlPageResponse<THtmlPageViewData>> Performer { get; set; }
        public IResponseWritter<IHtmlPageResponse<THtmlPageViewData>> ResponseWritter { get; set; }
        public IHtmlPage<THtmlPageViewData> HtmlPage { get; set; }
    }
    public abstract class HtmlPageEndpointBase<TRequest, THtmlPageViewData> 
        : IHtmlPageEndpoint<TRequest, THtmlPageViewData>
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<TRequest, IHtmlPageResponse<THtmlPageViewData>> Performer { get; set; }
        public IResponseWritter<IHtmlPageResponse<THtmlPageViewData>> ResponseWritter { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IRequestValidator<TRequest> RequestValidator { get; set; }
        public IRequestFailureHandler<TRequest, IHtmlPageResponse<THtmlPageViewData>> RequestFailureHandler { get; set; }
        public IHtmlPage<THtmlPageViewData> HtmlPage { get; set; }
    }

}
