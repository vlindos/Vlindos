using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Extensions.IEnumerable;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestProcessor<in THttpEndpoint>
        where THttpEndpoint : IHttpEndpoint
    {
        void ProcessHttpRequest(HttpContext httpContext, THttpEndpoint httpEndpoint);
    }

    public class HttpRequestProcessor<TResponse> : IHttpRequestProcessor<IHttpEndpoint<TResponse>>
    {
        private readonly IResponseHeadersWritter _responseHeadersWritter;

        public HttpRequestProcessor(IResponseHeadersWritter responseHeadersWritter)
        {
            _responseHeadersWritter = responseHeadersWritter;
        }
        public void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint<TResponse> httpEndpoint)
        {
            TResponse response;
            if (httpEndpoint.BeforePerformActions != null &&
                httpEndpoint.BeforePerformActions
                    .Any(beforePerformAction => beforePerformAction.PrePerform(httpContext) == false))
            {
                response = default(TResponse);
            }
            else
            {
                response = httpEndpoint.Performer.Perform();
            } 

            if (httpEndpoint.AfterPerformActions != null)
            {
                httpEndpoint.AfterPerformActions
                    .DoUntil(afterPerformAction => afterPerformAction.PostPerform(httpContext) == false);
            }

            _responseHeadersWritter.WriteResponseHeaders(httpContext);

            httpEndpoint.ResponseWritter.WriteResponse(httpContext, response);
        }
    }

    public class HttpRequestProcessor<TRequest, TResponse> : IHttpRequestProcessor<IHttpEndpoint<TRequest, TResponse>>
    {
        private readonly IResponseHeadersWritter _responseHeadersWritter;

        public HttpRequestProcessor(IResponseHeadersWritter responseHeadersWritter)
        {
            _responseHeadersWritter = responseHeadersWritter;
        }
        public void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint<TRequest, TResponse> httpEndpoint)
        {
            TResponse response;
            if (httpEndpoint.BeforePerformActions != null &&
                httpEndpoint.BeforePerformActions
                    .Any(beforePerformAction => beforePerformAction.PrePerform(httpContext) == false))
            {
                response = default(TResponse);
            }
            else
            {
                var messages = new List<string>();
                TRequest request;
                if (httpEndpoint.HttpRequestUnbinder.TryToUnbind(
                    httpContext.HttpRequest, messages, out request) == false)
                {
                    response = httpEndpoint.RequestFailureHandler.HandleRequestFailure(
                        httpContext, RequestFailedAt.PreAction, messages, request);
                }
                else
                {
                    if (httpEndpoint.RequestValidator.Validate(request, messages) == false)
                    {
                        response = httpEndpoint.RequestFailureHandler.HandleRequestFailure(
                            httpContext, RequestFailedAt.PreAction, messages, request);
                    }
                    else
                    {
                        response = httpEndpoint.Performer.Perform(request);
                    }
                }
            }

            if (httpEndpoint.AfterPerformActions != null)
            {
                httpEndpoint.AfterPerformActions
                    .DoUntil(afterPerformAction => afterPerformAction.PostPerform(httpContext) == false);
            }

            _responseHeadersWritter.WriteResponseHeaders(httpContext);

            httpEndpoint.ResponseWritter.WriteResponse(httpContext, response);
        }
    }
}