using System.Collections.Generic;
using System.Net;
using Framework.Web.Application.ServiceEndpoint.Models;
using Framework.Web.Models;

namespace Framework.Web.Application.ServiceEndpoint.Filters
{
    public interface IServiceRequestHandler<TRequest, TResponse> : IAfterPerformServiceEndpointFilter<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
    }

    public class ServiceRequestHandler<TRequest, TResponse> : IServiceRequestHandler<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        public int Priority { get { return 100; } }

        public bool BeforePerform(
            IHttpRequest httpRequest, IHttpResponse httpResponse, IServerSideServiceEndpoint<TRequest, TResponse> serviceEndpoint)
        {
            var unbinder = serviceEndpoint.HttpRequestUnbinder;

            // does the enpoint expects input?
            if (unbinder == null) return true;

            var messages = new List<string>();
            TRequest request;
            if (unbinder.TryToUnbind(httpRequest, out request, messages) == false)
            {
                messages.Add("Bad request.");
                var response = default(TResponse);
                response.Messages = messages;

                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                httpResponse.FiltersObjects.Add(messages);

                return false;
            }

            var validator = serviceEndpoint.ServiceEndpoint.RequestValidator;
            if (validator == null) // does the enpoint requires validation?
            {
                return true;
            }

            if (validator.Validate(request, messages) == false)
            {
                messages.Add("Invalid request.");
                var response = default(TResponse);
                response.Messages = messages;

                httpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
                httpResponse.FiltersObjects.Add(messages);
                return false;
            }

            httpRequest.FiltersObjects.Add(request);

            return true;
        }
    }
}