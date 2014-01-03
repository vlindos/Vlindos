using System.Collections.Generic;
using System.Text;
using Framework.Web.Application;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.DemoApp.Endpoints.Grep
{
    public interface IGrepRequestFailureHandler : IRequestFailureHandler<GrepRequest, string>
    {
    }

    public class GrepRequestFailureHandler : IGrepRequestFailureHandler
    {
        public string HandleRequestFailure(
            HttpContext httpContext, RequestFailedAt requestFailedAt, List<string> messages, GrepRequest request)
        {
            var sb = new StringBuilder();
            switch (requestFailedAt)
            {
                case RequestFailedAt.PreAction:
                    sb.AppendLine("Preparing for request handling.");  
                    break;
                case RequestFailedAt.Unbinding:
                    sb.AppendLine("Unable to read request.");  
                    break;
                case RequestFailedAt.Validation:
                    sb.AppendLine("Unable to validate request.");
                    break;
            }
            messages.ForEach(x => sb.AppendLine(x));
            return sb.ToString();
        }
    }
}