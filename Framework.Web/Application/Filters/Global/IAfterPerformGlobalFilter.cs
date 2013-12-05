using Framework.Web.Models;

namespace Framework.Web.Application.Filters.Global
{
    public interface IAfterPerformGlobalFilter : IAfterPerformFilter, IGlobalFilter
    {
        bool AfterPerform(IHttpRequest request, IHttpResponse httpResponse);
    }
}