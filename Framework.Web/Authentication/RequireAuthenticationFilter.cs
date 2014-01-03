using System.Net;
using System.Text;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Session;
using Framework.Web.Tools;

namespace Framework.Web.Authentication
{
    public interface IRequireAuthenticationFilter : IPrePerformAction
    {
    }

    public class RequireAuthenticationFilter : IRequireAuthenticationFilter
    {
        private readonly IApplicationRuntimeSettings _applicationRuntimeSettings;
        private readonly IBaseUrlGenerator _baseUrlGenerator;
        private readonly IAuthenticateServiceEndpoint _authenticateServiceEndpoint;
        private readonly ISessionGetter _sessionGetter;

        public RequireAuthenticationFilter(
            IApplicationRuntimeSettings applicationRuntimeSettings,
            IBaseUrlGenerator baseUrlGenerator,
            IAuthenticateServiceEndpoint authenticateServiceEndpoint, 
            ISessionGetter sessionGetter)
        {
            _applicationRuntimeSettings = applicationRuntimeSettings;
            _baseUrlGenerator = baseUrlGenerator;
            _authenticateServiceEndpoint = authenticateServiceEndpoint;
            _sessionGetter = sessionGetter;
        }

        public bool PrePerform(HttpContext httpContext)
        {
            var session = _sessionGetter.GetSession(httpContext);
            if (session["Authenticated"] == true.ToString()) // allow to continue
            {
                return true;
            }

            // redirect to authetnication endpoint and note it of the return url
            var baseUrl = _baseUrlGenerator.GenerateUrlBase(httpContext.HttpRequest);
            var authenticationUrl = new StringBuilder();
            authenticationUrl.Append(baseUrl);
            if (!string.IsNullOrWhiteSpace(_applicationRuntimeSettings.BaseAddress))
            {
                authenticationUrl.AppendFormat("/{0}", _applicationRuntimeSettings.BaseAddress);
            }
            if (!string.IsNullOrWhiteSpace(_authenticateServiceEndpoint.HttpRequestDescriptor.RouteDescription))
            {
                authenticationUrl.AppendFormat("/{0}", _authenticateServiceEndpoint.HttpRequestDescriptor.RouteDescription);
            }
            httpContext.HttpResponse.Headers.Add("Location: ", authenticationUrl.ToString());
            httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.Redirect;
            var returnUrl = new StringBuilder();
            returnUrl.Append(baseUrl);
            if (!string.IsNullOrWhiteSpace(httpContext.HttpRequest.Path))
            {
                authenticationUrl.AppendFormat("/{0}", httpContext.HttpRequest.Path);
            }
            session["ReturnUrl"] = returnUrl.ToString();

            return false;
        }
    }
}