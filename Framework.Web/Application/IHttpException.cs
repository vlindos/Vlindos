using System;

namespace Framework.Web.Application
{
    public interface IHttpException
    {
        void OnException(Exception exception, HttpContext httpContext);
    }
}
