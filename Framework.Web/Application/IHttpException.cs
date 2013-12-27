using System;
using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IHttpException
    {
        void OnException(Exception exception, HttpContext httpContext);
    }
}
