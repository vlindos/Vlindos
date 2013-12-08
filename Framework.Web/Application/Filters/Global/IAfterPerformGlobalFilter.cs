using Framework.Web.Models;

namespace Framework.Web.Application.Filters.Global
{
    public interface IAfterPerformGlobalFilter<TRequest, TResponse> : IAfterPerformFilter, IGlobalFilter
    {
        bool AfterPerform(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse);
    }
}