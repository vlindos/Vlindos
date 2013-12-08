using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IPerformerManager
    {
        TPerformer GetPerformer<TPerformer, TRequest, TResponse>(
            IHttpRequest<TRequest> request, IHttpResponse<TResponse> httpResponse)
            where TPerformer : IRequestPerformer<TResponse>;
    }
}
