using System.Collections.Generic;
using System.Net;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Service
{
    public interface IServiceRequestFailureHandler<in TRequest, out TResponse> 
        : IRequestFailureHandler<TRequest, TResponse>
            where TResponse : IServiceResponse
    {
    }

    public class ServiceRequestHandler<TRequest, TResponse> : IServiceRequestFailureHandler<TRequest, TResponse>
        where TResponse : IServiceResponse
    {
        public TResponse HandleRequestFailure(
            HttpContext httpContext, RequestFailedAt requestFailedAt, List<string> messages, TRequest request)
        {
            httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
            var serviceResponse = default(TResponse);
            serviceResponse.Messages = messages;
            serviceResponse.Success = false;

            return serviceResponse;
        }
    }
}