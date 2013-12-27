using Framework.Web.Models;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPage
    {
        void RenderPage(IRenderer renderer, HttpRequest httpRequest, HttpResponse httpResponse);
    }
}