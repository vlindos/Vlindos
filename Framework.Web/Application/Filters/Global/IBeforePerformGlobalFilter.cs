using Framework.Web.Models;

namespace Framework.Web.Application.Filters.Global
{
    public interface IBeforePerformGlobalFilter : IAfterPerformFilter, IGlobalFilter
    {
    }
}