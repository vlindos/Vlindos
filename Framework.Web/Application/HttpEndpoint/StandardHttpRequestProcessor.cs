using System.Collections.Generic;
using System.Linq;
using Vlindos.Common.Extensions.IEnumerable;

namespace Framework.Web.Application.HttpEndpoint
{
    // http://dotnet-snippets.com/snippet/runtime-compilation/638 ??
    public interface IStandardHttpRequestProcessor : IHttpRequestProcessor
    {
    }

    public class StandardHttpRequestProcessor : IStandardHttpRequestProcessor
    {
        private readonly IResponseHeadersWritter _responseHeadersWritter;

        public StandardHttpRequestProcessor(IResponseHeadersWritter responseHeadersWritter)
        {
            _responseHeadersWritter = responseHeadersWritter;
        }

        public void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint httpEndpoint)
        {

            var htmlEndpointType = httpEndpoint.GetType();

            var requestFailureHandlerProperty = htmlEndpointType.GetProperty("RequestFailureHandler");
            var requestFailureHandlerGetter = requestFailureHandlerProperty.GetGetMethod();
            var requestFailureHandler = requestFailureHandlerGetter.Invoke(
                htmlEndpointType, new object[] { });
            var requestFailureMethod = requestFailureHandler.GetType().GetMethod("HandleRequestFailure");

            object response;

            if (httpEndpoint.BeforePerformActions != null &&
                httpEndpoint.BeforePerformActions
                    .Any(beforePerformAction => beforePerformAction.PrePerform(httpContext) == false))
            {
                response = requestFailureMethod.Invoke(
                    requestFailureHandler, new object[] { httpContext, RequestFailedAt.PreAction, new List<string>(), null });
            }
            else
            {
                var performerProperty = htmlEndpointType.GetProperty("Performer");
                var performerGetter = performerProperty.GetGetMethod();
                var performer = performerGetter.Invoke(htmlEndpointType, new object[] { });
                var performMethod = performer.GetType().GetMethod("Perform");
                var methodArgs = new List<object>();
                if (htmlEndpointType.IsGenericType)
                {
                    var httpRequestUnbinderProperty = htmlEndpointType.GetProperty("HttpRequestUnbinder");
                    if (httpRequestUnbinderProperty != null)
                    {
                        var httpRequestUnbinderGetter = httpRequestUnbinderProperty.GetGetMethod();
                        var httpRequestUnbinder = httpRequestUnbinderGetter.Invoke(htmlEndpointType, new object[] { });
                        var httpRequestUnbinderArgs = new object[] { httpContext.HttpRequest, new List<string>(), null };

                        var tryToUnbindMethod = httpRequestUnbinder.GetType().GetMethod("TryToUnbind");
                        var unbindResult = (bool)tryToUnbindMethod.Invoke(httpRequestUnbinder, httpRequestUnbinderArgs);
                        if (!unbindResult)
                        {
                            requestFailureMethod.Invoke(requestFailureHandler,
                                new[] { httpContext, RequestFailedAt.Unbinding, new List<string>(), httpRequestUnbinderArgs[1] });
                            return;
                        }

                        var request = httpRequestUnbinderArgs[2];
                        var requestValidatorProperty = htmlEndpointType.GetProperty("RequestValidator");
                        if (requestValidatorProperty != null)
                        {
                            var requestValidatorGetter = requestValidatorProperty.GetGetMethod();
                            var requestValidator = requestValidatorGetter.Invoke(htmlEndpointType, new object[] { });
                            var requestValidatorArgs = new[] { request, new List<string>() };

                            var validateMethod = requestValidator.GetType().GetMethod("TryToUnbind");
                            var validateResult = (bool)validateMethod.Invoke(requestValidator, requestValidatorArgs);
                            if (validateResult == false)
                            {
                                requestFailureMethod.Invoke(requestFailureHandler,
                                    new[] { httpContext, RequestFailedAt.Validation, new List<string>(), httpRequestUnbinderArgs[1] });
                                return;
                            }
                        }
                    }
                }

                response = performMethod.Invoke(performer, methodArgs.ToArray());
            }

            if (httpEndpoint.AfterPerformActions != null)
            {
                httpEndpoint.AfterPerformActions
                    .DoUntil(afterPerformAction => afterPerformAction.PostPerform(httpContext) == false);
            }

            _responseHeadersWritter.WriteResponseHeaders(httpContext);
            
            var responseWritterProperty = htmlEndpointType.GetProperty("ResponseWritter");
            var responseWritterGetter = responseWritterProperty.GetGetMethod();
            var responseWritter = responseWritterGetter.Invoke(htmlEndpointType, new object[] { });
            var writeResponseMethod = responseWritter.GetType().GetMethod("WriteResponse");
            writeResponseMethod.Invoke(responseWritter, new[] { httpContext, response });
        }
    }
}