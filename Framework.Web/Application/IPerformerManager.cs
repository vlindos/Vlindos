using Framework.Web.Models;

namespace Framework.Web.Application
{
    public interface IPerformerManager
    {
        T GetPerformer<T>(HttpRequest request, HttpResponse httpResponse)
            where T : IRequestPerformer;
    }
}
