using System;
using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IExceptionFilter : IFilter
    {
        HttpResponse OnException(HttpRequest request, Exception exception);
    }
}
