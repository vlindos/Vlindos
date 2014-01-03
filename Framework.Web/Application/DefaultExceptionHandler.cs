using System;
using Framework.Web.Application.HttpEndpoint;

namespace Framework.Web.Application
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public void OnException(Exception exception, HttpContext httpContext, IHttpEndpoint httpEndpoint)
        {
            throw new NotImplementedException();
        }
    }
}