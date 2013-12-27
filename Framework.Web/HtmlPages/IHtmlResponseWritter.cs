using Framework.Web.Application.HttpEndpoint;
using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlResponseWritter : IResponseWritter
    {
    }

    public class HtmlResponseWritter : IHtmlResponseWritter
    {
        private readonly IRendererFactory _rendererFactory;

        public HtmlResponseWritter(IRendererFactory rendererFactory)
        {
            _rendererFactory = rendererFactory;
        }

        public void WriteResponse(HttpRequest httpRequest, HttpResponse httpResponse)
        {
            var renderer = _rendererFactory.GetRenderer();
            httpResponse.Response.HtmlPage.RenderPage(renderer, httpRequest, httpResponse);
        }
    }
}