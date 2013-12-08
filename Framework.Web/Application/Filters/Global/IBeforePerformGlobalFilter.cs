using Framework.Web.Models;

namespace Framework.Web.Application.Filters.Global
{
    public interface IBeforePerformGlobalFilter<TRequest, TResponse> : IAfterPerformFilter
    {
        bool BeforePerform(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse);
    }
}