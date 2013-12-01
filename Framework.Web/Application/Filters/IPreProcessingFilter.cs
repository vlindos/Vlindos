using Framework.Web.Application.Endpoints;
using Framework.Web.Models;

namespace Framework.Web.Application.Filters
{
    public interface IPreProcessingFilter : IFilter
    {
        HttpResponse PreProcessRequest(HttpRequest request);
    }
}