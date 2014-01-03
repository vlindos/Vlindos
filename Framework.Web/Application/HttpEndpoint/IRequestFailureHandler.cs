using System.Collections.Generic;

namespace Framework.Web.Application.HttpEndpoint
{
    public enum RequestFailedAt
    {
        PreAction,
        Unbinding,
        Validation
    }

    public interface IRequestFailureHandler<in TRequest, out TResponse>
    {
        TResponse HandleRequestFailure(
            HttpContext httpContext, RequestFailedAt requestFailedAt, List<string> messages, TRequest request);
    }
}