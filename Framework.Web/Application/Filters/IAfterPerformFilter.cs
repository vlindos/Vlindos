using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IAfterPerformFilter : IFilter
    {
        bool AfterPerform<TRequest, TResponse>(IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse);
    }
}