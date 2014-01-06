using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Assets;

namespace Framework.Web.Service
{
    public abstract class ServiceEndpointBase<TResponse> 
        : IHttpEndpoint<TResponse>
            where TResponse : IServiceResponse
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public IResponseWritter<TResponse> ResponseWritter { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<TResponse> Performer { get; set; }
    }

    public interface IServiceEndpoint<TRequest, TResponse> 
        : IHttpEndpoint<TRequest, TResponse>
    {
        IJavascriptProvider JavascriptValidatorProvider { get; set; }
    }

    public abstract class ServiceEndpointBase<TRequest, TResponse>
        : IServiceEndpoint<TRequest, TResponse>
            where TResponse : IServiceResponse
    {
        public IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
        public IHttpRequestProcessor HttpRequestProcessor { get; set; }
        public IResponseWritter<TResponse> ResponseWritter { get; set; }
        public List<IPrePerformAction> BeforePerformActions { get; set; }
        public List<IPostPerformAction> AfterPerformActions { get; set; }
        public IPerformer<TRequest, TResponse> Performer { get; set; }
        public IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }
        public IRequestValidator<TRequest> RequestValidator { get; set; }
        public IRequestFailureHandler<TRequest, TResponse> RequestFailureHandler { get; set; }
        public IJavascriptProvider JavascriptValidatorProvider { get; set; }
    }
}
