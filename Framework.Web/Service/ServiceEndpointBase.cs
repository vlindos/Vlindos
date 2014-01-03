using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Service
{
    public abstract class ServiceEndpointBase<TResponse> : IHttpEndpoint<TResponse>
        where TResponse : IServiceResponse
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public IResponseWritter<TResponse> ResponseWritter { get; set; }
        public List<IBeforePerformAction> BeforePerformActions { get; set; }
        public List<IAfterPerformAction> AfterPerformActions { get; set; }
        public IPerformer<TResponse> Performer { get; set; }
    }

    public abstract class ServiceEndpointBase<TRequest, TResponse> : IHttpEndpoint<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public IResponseWritter<TResponse> ResponseWritter { get; set; }
        public List<IBeforePerformAction> BeforePerformActions { get; set; }
        public List<IAfterPerformAction> AfterPerformActions { get; set; }
        public IPerformer<TRequest, TResponse> Performer { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IRequestValidator<TRequest> RequestValidator { get; set; }
        public IRequestFailureHandler<TRequest> RequestFailureHandler { get; set; }
    }
}
