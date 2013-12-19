using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IAfterPerformFilter : IFilter
    {
        bool AfterPerform(HttpRequest request, HttpResponse httpResponse);
    }
}