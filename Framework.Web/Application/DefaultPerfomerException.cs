using System;

namespace Framework.Web.Application
{
    public class DefaultPerfomerException : IHttpException
    {
        public void OnException(Exception exception, HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}