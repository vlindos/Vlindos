using System.Collections.Generic;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models.HttpMethods;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IServerSideHttpEndpoint
    {
    }

    public interface IServerSideHttpEndpoint<TRequest, TResponse> : IServerSideHttpEndpoint
    {
        IHttpMethod[] HttpMethods { get; set; }

        IHttpEndpoint<TRequest> HttpEndpoint { get; }

        IHttpRequestUnbinder<TRequest> HttpRequestUnbinder { get; }

        IHttpStreamResponseWritter<TRequest, TResponse> HttpStreamResponseWritter { get; }

        IRequestPerformer<TResponse> RequestPerformer { get; }

        List<IHttpEndpointFilter> Filters { get; } 
    }
}