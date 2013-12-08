using System;
using System.Net;
using Framework.Web.Application.HttpEndpoint.Filters;
using Framework.Web.DemoApp.Endpoints.Authenticate;
using Framework.Web.Models;

namespace Framework.Web.DemoApp.Filters
{
    public interface IAuthenticationFilter : IBeforePerformHttpEndpointFilter, IAfterPerformHttpEndpointFilter
    {
    }

    public class AuthenticationFilter : IAuthenticationFilter
    {
        private readonly IAuthenticateRestEndpoint _authenticateRestEndpoint;

        public AuthenticationFilter(IAuthenticateRestEndpoint authenticateRestEndpoint)
        {
            _authenticateRestEndpoint = authenticateRestEndpoint;
        }

        public int Priority { get { return 10000; } }
        public bool AfterPerform<TRequest, TResponse>(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse)
        {
            if (request.Endpoint != _authenticateRestEndpoint)
            {
                return true;
            }

            if (request.Session["Authenticated"] == false.ToString())
            {
                return true;
            }

            var newLocation = request.Session["AfterAuthenticationLocation"];

            httpResponse.Headers.Add("Location:", newLocation);

            return false;
        }

        public bool BeforePerform<TRequest, TResponse>(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse)
        {
            if (request.Session["Authenticated"] == true.ToString())
            {
                return true;
            }
            httpResponse.HttpStatusCode = HttpStatusCode.Forbidden;
            request.Session["AfterAuthenticationLocation"] = request.RawUrl;
            httpResponse.Headers.Add("Location:", _authenticateRestEndpoint.HttpEndpoint.HttpUrlDescription);
            return false;
        }
    }
}