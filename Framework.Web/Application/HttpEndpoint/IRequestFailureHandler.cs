using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public interface IRequestFailureHandler<in TRequest>
    {
        void UnbindFailure(HttpContext httpContext, List<string> messages, TRequest request);
        void ValidateFailure(HttpContext httpContext, List<string> messages, TRequest request);
    }
}