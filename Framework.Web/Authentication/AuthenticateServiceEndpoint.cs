using System.Collections.Generic;
using System.Net;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.HttpMethods;
using Framework.Web.Service;
using Framework.Web.Session;
using Framework.Web.Tools;
using Vlindos.Common.Models;

namespace Framework.Web.Authentication
{
    public interface IAuthenticateServiceEndpoint 
        : IHttpEndpoint<UsernamePassword, ServiceResponse>
    {
    }

    public class AuthenticateServiceEndpoint 
        : ServiceEndpointBase<UsernamePassword, ServiceResponse>, IAuthenticateServiceEndpoint
    {
        public AuthenticateServiceEndpoint(
            IServiceEndpointBootstrapper<UsernamePassword, ServiceResponse> bootstrapper,
            IUserRepository userRepository,
            IFormDataReader formDataReader, 
            IPostHttpMethod postHttpMethod, 
            ISessionGetter sessionGetter)
        {
            bootstrapper.Bootstrap(this, postHttpMethod, "authenticate",
                (HttpContext httpContext, IList<string> messages, out UsernamePassword request) =>
                {
                    Dictionary<string, string> formData;
                    if (formDataReader.ReadFormData(httpContext.HttpRequest.InputStream, messages, out formData) == false)
                    {
                        request = null;
                        return false;
                    }
                    request = new UsernamePassword
                    {
                        Username = formData["username"],
                        Password = formData["password"]
                    };
                    return true;
                },
                perform: (httpContext, request) =>
                {
                    var session = sessionGetter.GetSession(httpContext);
                    if (session["Authenticated"] == true.ToString())
                    {
                        return new ServiceResponse
                        {
                            Success = true,
                            Messages = new List<string> { "Already authenticated" }
                        };
                    }
                    if (userRepository.Authenticate(request.Username, request.Password))
                    {
                        session["Username"] = request.Username;
                        session["Authenticated"] = true.ToString();
                        return new ServiceResponse
                        {
                            Success = true,
                            Messages = new List<string> { "Successfully authenticated" }
                        };
                    }
                    httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.Forbidden;
                    return new ServiceResponse
                    {
                        Success = false,
                        Messages = new List<string> { "Invalid username or password" }
                    };
                });
        }
    }
}