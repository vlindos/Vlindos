using Framework.Web.Models;

namespace Framework.Web.Application.Filters.Global
{
    public interface IBeforePerformGlobalFilter : IAfterPerformFilter
    {
        bool BeforePerform(IHttpRequest request, IHttpResponse httpResponse);
    }
}