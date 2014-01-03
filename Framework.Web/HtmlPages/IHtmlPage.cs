using Framework.Web.Application;

namespace Framework.Web.HtmlPages
{
    public interface IHtmlPage<in THtmlPageViewData> 
    {
        void RenderPage(HttpContext httpContext, IHtmlPageRenderer htmlPageRenderer, THtmlPageViewData viewData);
    }
}