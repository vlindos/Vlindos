using System.Collections.Generic;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Tools;

namespace Framework.Web.DemoApp.Endpoints.Grep
{
    public interface IGrepRequestFailureHandler : IRequestFailureHandler<GrepRequest>
    {
    }

    public class GrepRequestFailureHandler : IGrepRequestFailureHandler
    {
        private readonly IStringResponseWritter _stringResponseWritter;

        public GrepRequestFailureHandler(IStringResponseWritter stringResponseWritter)
        {
            _stringResponseWritter = stringResponseWritter;
        }

        public void UnbindFailure(HttpContext httpContext, List<string> messages, GrepRequest request)
        {
            _stringResponseWritter.WriteResponse(httpContext, "Unable to read request.");  
            foreach (var message in messages)
            {
                _stringResponseWritter.WriteResponse(httpContext, message);   
            }
        }

        public void ValidateFailure(HttpContext httpContext, List<string> messages, GrepRequest request)
        {
            _stringResponseWritter.WriteResponse(httpContext, "Unable to validate request.");  
            foreach (var message in messages)
            {
                _stringResponseWritter.WriteResponse(httpContext, message);
            }
        }
    }
}