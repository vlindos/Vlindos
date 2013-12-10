using System.Collections.Generic;
using System.Net;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;
using Framework.Web.Models.HttpMethods;
using Framework.Web.Service;
using Framework.Web.Service.Rest.Models;
using Framework.Web.Service.Rest.Tools;
using Framework.Web.Tools;
using Vlindos.Common.Models;

namespace Framework.Web.DemoApp.Endpoints.Authenticate
{
    public interface IAuthenticateRestEndpoint : IServerSideHttpEndpoint<UsernamePassword, AuthenticateResponse>
    {
        
    }

    public class AuthenticateRestEndpoint : RestEndpointBase<UsernamePassword, AuthenticateResponse>, IAuthenticateRestEndpoint
    {
        public AuthenticateRestEndpoint(
            IRestEndpointBootstrapper bootstrapper, 
            IFormDataReader formDataReader, 
            IPostHttpMethod postHttpMethod, 
            IJsonResponseWritter<UsernamePassword, AuthenticateResponse> jsonResponseWritter)
        {
        //    bootstrapper.Bootstrap<UsernamePassword, AuthenticateResponse>(this, postHttpMethod, "authenticate",
        //        unbind: (httpRequest, messages) =>
        //        {
        //            httpRequest.Request = new UsernamePassword
        //            {
        //                Username = formDataReader.ReadFrom(httpRequest.InputStream, "username"),
        //                Password = formDataReader.ReadFrom(httpRequest.InputStream, "password")
        //            };
        //            return true;
        //        },
        //        perform: (IHttpRequest<UsernamePassword> httpRequest, IHttpResponse<AuthenticateResponse> httpResponse) =>
        //        {
        //            if (httpRequest.Session["Authenticated"] == true.ToString())
        //            {
        //                httpResponse.Response = new AuthenticateResponse
        //                {
        //                    Success = true, 
        //                    Messages = new List<string> { "Already authenticated" }
        //                };
        //                return;
        //            }
        //            var request = httpRequest.Request;
        //            if (request.Username == "vladimir.moushkov" && request.Password == "vlad's pass")
        //            {
        //                httpRequest.Session["Username"] = request.Username;
        //                httpRequest.Session["Authenticated"] = true.ToString();
        //                httpResponse.Response = new AuthenticateResponse { Success = true };
        //                return;
        //            }
        //            httpResponse.HttpStatusCode = HttpStatusCode.Forbidden;
        //            httpResponse.Response = new AuthenticateResponse
        //            {
        //                Success = false,
        //                Messages = new List<string> {"Invalid username or password" }
        //            };
        //        },
        //        responseWritter: jsonHttpStreamResponseWritter);
        }
    }
}