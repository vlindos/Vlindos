using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IHttpRequestProcessor
    {
        void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint httpEndpoint);
    }

    public class HttpRequestProcessor : IHttpRequestProcessor
    {
        private readonly IResponseHeadersWritter _responseHeadersWritter;

        public HttpRequestProcessor(IResponseHeadersWritter responseHeadersWritter)
        {
            _responseHeadersWritter = responseHeadersWritter;
        }

        public void ProcessHttpRequest(HttpContext httpContext, IHttpEndpoint httpEndpoint)
        {
            if (httpEndpoint.BeforePerformActions != null && 
                httpEndpoint.BeforePerformActions
                            .Any(beforePerformAction => beforePerformAction.BeforePerformAction(httpContext) == false))
            {
                _responseHeadersWritter.WriteResponseHeaders(httpContext);
            
                return;
            }

            var htmlEndpointType = httpEndpoint.GetType();
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
                        var requestFailureHandlerProperty = htmlEndpointType.GetProperty("RequestFailureHandler");
                        var requestFailureHandlerGetter = requestFailureHandlerProperty.GetGetMethod();
                        var requestFailureHandler = requestFailureHandlerGetter.Invoke(
                            htmlEndpointType, new object[] { });
                        var unbindFailureMethod = requestFailureHandler.GetType().GetMethod("UnbindFailure");
                        unbindFailureMethod.Invoke(requestFailureHandler,
                            new[] { httpContext, httpEndpoint, httpRequestUnbinderArgs[1] });
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
                            var requestFailureHandlerProperty = htmlEndpointType.GetProperty("RequestFailureHandler");
                            var requestFailureHandlerGetter = requestFailureHandlerProperty.GetGetMethod();
                            var requestFailureHandler = requestFailureHandlerGetter.Invoke(
                                htmlEndpointType, new object[] { });

                            var unbindFailureMethod = requestFailureHandler.GetType().GetMethod("ValidateFailure");
                            unbindFailureMethod.Invoke(requestFailureHandler,
                                new[] { httpContext, httpEndpoint, httpRequestUnbinderArgs[1] });
                            return;
                        }
                    }
                }
            }

            var response = performMethod.Invoke(performer, methodArgs.ToArray());

            if (httpEndpoint.AfterPerformActions != null &&
                httpEndpoint.AfterPerformActions
                            .Any(afterPerformAction => afterPerformAction.AfterPerformAction(httpContext) == false))
            {
                _responseHeadersWritter.WriteResponseHeaders(httpContext);
            
                return;
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