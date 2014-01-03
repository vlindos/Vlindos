using System.Collections.Generic;
using System.Net;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Service
{
    public interface IServiceRequestHandler<in TRequest> : IRequestFailureHandler<TRequest>
    {
    }

    public class ServiceRequestHandler<TRequest> : IRequestFailureHandler<TRequest>
    {
        public void UnbindFailure(HttpContext httpContext, List<string> messages, TRequest request)
        {
            httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public void ValidateFailure(HttpContext httpContext, List<string> messages, TRequest request)
        {
            httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.BadRequest;
        }
    }
}