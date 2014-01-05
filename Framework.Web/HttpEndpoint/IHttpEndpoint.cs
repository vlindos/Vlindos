using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Service;

namespace Framework.Web.HttpEndpoint
{
    public interface IHttpEndpoint
    {
        IHttpRequestDescriptor HttpRequestDescriptor { get; set; }
    }


    public interface IHttpEndpoint<TResponse> : IHttpEndpoint
    {
        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; set; }
    }

    public interface IHttpEndpoint<TRequest, TResponse> : IHttpEndpoint
    {
        IHttpRequestBuilder<TRequest> HttpRequestBuilder { get; set; }

        IRequestValidator<TRequest> RequestValidator { get; set; }

        IHttpStreamResponseReader<TResponse> HttpStreamResponseReader { get; set; }
    }
}