using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IPostProcessingFilter : IFilter
    {
        HttpResponse PostProcessRequest(HttpRequest request);
    }
}