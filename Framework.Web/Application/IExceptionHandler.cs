using System;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Application
{
    public interface IExceptionHandler
    {
        void OnException(Exception exception, HttpContext httpContext, IHttpEndpoint httpEndpoint);
    }
}
