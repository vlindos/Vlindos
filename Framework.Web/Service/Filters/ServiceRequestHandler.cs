using System.Collections.Generic;
using System.Net;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.Models;
using Framework.Web.Service.Models;

namespace Framework.Web.Service.Filters
{
    public interface IServiceRequestHandler : IBeforePerformHttpEndpointFilter
    {
    }

    public class ServiceRequestHandler : IServiceRequestHandler
    {
        public int Priority { get { return 5000; } }

        public bool BeforePerform(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            if (typeof(TResponse).IsAssignableFrom(typeof(IServiceResponse))) return true;
            var httpEndpoint = httpRequest.Endpoint;
            // is request having IServiceResponse?
            if (httpEndpoint == null) return true;

            var unbinder = httpEndpoint.HttpRequestUnbinder;

            // does the enpoint expects input?
            if (unbinder == null) return true;

            var messages = new List<string>();
            if (unbinder.TryToUnbind(httpRequest, messages) == false)
            {
                messages.Add("Bad request.");
                var response = (IServiceResponse)default(TResponse);
                response.Messages = messages;

                httpResponse.Response = (TResponse)response;
                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;

                return false;
            }

            var validator = httpEndpoint.HttpEndpoint.RequestValidator;
            if (validator == null) // does the enpoint requires validation?
            {
                return true;
            }

            if (validator.Validate(httpRequest.Request, messages) == false)
            {
                messages.Add("Invalid request.");
                var response = (IServiceResponse)default(TResponse);
                response.Messages = messages;

                httpResponse.Response = (TResponse)response;
                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                return false;
            }

            return true;
        }
    }
}