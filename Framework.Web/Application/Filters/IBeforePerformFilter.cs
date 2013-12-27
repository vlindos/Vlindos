using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IBeforePerformFilter : IFilter
    {
        bool BeforePerform(HttpContext httpContext);
    }
}