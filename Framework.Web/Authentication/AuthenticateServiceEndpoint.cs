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
                (HttpContext httpContext, List<string> messages, out UsernamePassword request) =>
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
                    if (request.Username != null)
                    {
                        request.Username = request.Username.Trim();
                    }
                    return true;
                },
                perform: (httpContext, request) =>
                {
                    ServiceResponse serviceResponse;
                    var session = sessionGetter.GetSession(httpContext);
                    if (session["Authenticated"] == true.ToString())
                    {
                        serviceResponse = new ServiceResponse
                        {
                            Success = true,
                            Messages = new List<string> { "Already authenticated" }
                        };
                    } 
                    else if (userRepository.Authenticate(request.Username, request.Password))
                    {
                        session["Username"] = request.Username;
                        session["Authenticated"] = true.ToString();
                        serviceResponse = new ServiceResponse
                        {
                            Success = true,
                            Messages = new List<string> {"Successfully authenticated"}
                        };
                    }
                    else
                    {
                        httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.Forbidden;
                        serviceResponse = new ServiceResponse
                        {
                            Success = false,
                            Messages = new List<string> { "Invalid username or password" }
                        };   
                    }

                    var returnUrl = session["ReturnUrl"];
                    if (serviceResponse.Success && returnUrl != null)
                    {
                        session.Remove("ReturnUrl");
                        httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.Redirect;
                        httpContext.HttpResponse.Headers.Add("Location:", returnUrl);
                    }

                    return serviceResponse;
                },
                validate: (request, messages) =>
                {
                    if (string.IsNullOrWhiteSpace(request.Username))
                    {
                        messages.Add("No 'username' is set.");

                        return false;
                    }

                    return true;
                },
                requestFailureHandler: (httpContext, messages, request) =>
                {
                    httpContext.HttpResponse.HttpStatusCode = HttpStatusCode.Forbidden;
                    var serviceResponse = new ServiceResponse
                    {
                        Success = false,
                        Messages = new List<string>()
                    };
                    serviceResponse.Messages.AddRange(messages);
                    return serviceResponse;
                });
        }
    }
}