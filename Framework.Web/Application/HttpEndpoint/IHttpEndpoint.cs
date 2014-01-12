using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpEndpoint
    {
        // Describing the http request it accepts 
        // E.g. GET <WebAppPath>Help/{ApplicationName}
        IHttpRequestDescriptor HttpRequestDescriptor { get; set; }

        List<IPrePerformAction> BeforePerformActions { get; set; }

        List<IPostPerformAction> AfterPerformActions { get; set; } 
    }

    public interface IHttpEndpoint<TResponse> : IHttpEndpoint
    {
        IHttpRequestProcessor<IHttpEndpoint<TResponse>> HttpRequestProcessor { get; set; }

        IPerformer<TResponse> Performer { get; set; }

        IResponseWritter<TResponse> ResponseWritter { get; set; }
    }

    public interface IHttpEndpoint<TRequest, TResponse> : IHttpEndpoint
    {
        IHttpRequestProcessor<IHttpEndpoint<TRequest, TResponse>> HttpRequestProcessor { get; set; }

        IPerformer<TRequest, TResponse> Performer { get; set; }

        IResponseWritter<TResponse> ResponseWritter { get; set; }

        IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; set; }

        IRequestValidator<TRequest> RequestValidator { get; set; }

        IRequestFailureHandler<TRequest, TResponse> RequestFailureHandler { get; set; }
    }
}