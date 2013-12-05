using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IPerformerManager
    {
        TPerformer GetPerformer<TPerformer,TResponse>(IHttpRequest request, IHttpResponse httpResponse)
            where TPerformer : IRequestPerformer<TResponse>;
    }
}