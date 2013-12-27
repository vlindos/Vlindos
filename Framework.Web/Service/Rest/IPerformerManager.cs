using Framework.Web.Models;

namespace Framework.Web.Service.Rest
{
    public interface IPerformerManager
    {
        T GetPerformer<T, TResponse>(HttpContext httpContext)
            where T : IRequestPerformer<TResponse>;
    }
}
